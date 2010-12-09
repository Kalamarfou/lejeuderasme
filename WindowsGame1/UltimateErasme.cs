using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using UltimateErasme.GameObjects;
using UltimateErasme.ClassesDInternet.Particles;
using UltimateErasme.Collisions;
using UltimateErasme.XP;
using System.Threading;
using UltimateErasme.Network;
using UltimateErasme.Life;
using UltimateErasme.Cinematiques;
using System.Xml.Linq;


namespace UltimateErasme
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class UltimateErasme : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch { get; set; }
        public Rectangle viewportRect;

        public PlayersManager playerManager;
        public MechantManager mechantManager;
        public DecorsManager decorsManager;
        public ExplosionManager explosionManager;
        public CollisionsManager collisionsManager;
        public CinematiquesManager cinematiquesManager;
        static public XpManager xpManager;
        static public LifeManager lifeManager;

        public bool isPaused = false;
        bool isPausedByGuide = false;
        GameObject pauseImage;
        KeyboardState previousKeyboardState = Keyboard.GetState();

        const int maxGamers = 16;
        const int maxLocalGamers = 1;

        NetworkSession networkSession;
        PacketWriter packetWriter = new PacketWriter();
        PacketReader packetReader = new PacketReader();
        SpriteFont networkFont;
        string errorMessage = "";

        public UltimateErasme()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            Components.Add(new GamerServicesComponent(this));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewportRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            playerManager = new PlayersManager(this, viewportRect);
            mechantManager = new MechantManager(this, viewportRect);
            decorsManager = new DecorsManager(this, viewportRect);
            explosionManager = new ExplosionManager(this);
            collisionsManager = new CollisionsManager(this, viewportRect);
            cinematiquesManager = new CinematiquesManager(this);
            this.Components.Add(cinematiquesManager);
            xpManager = new XpManager(this);
            lifeManager = new LifeManager(this);
            

            pauseImage = new GameObject(this.Content.Load<Texture2D>(@"Sprites\Pause\Pause"));
            pauseImage.Position = new Vector2(0, 80);

            networkFont = Content.Load<SpriteFont>("Fonts/NetworkFont");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F3))
                errorMessage = "";

            //TODO
            if (Keyboard.GetState().IsKeyDown(Keys.C))
                cinematiquesManager.playCinematic(@"Content\DialoguesXML\DialogueDebut.xml");

            PauseManager(gameTime);

            NetworkSessionManager(gameTime);

            if (!isPaused)
            {
                collisionsManager.Update(gameTime);
                decorsManager.Update(gameTime);
                playerManager.Update(gameTime);
                mechantManager.Update(gameTime);
                explosionManager.Update(gameTime);
                xpManager.Update(gameTime);
                //lifeManager.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void SetPause(bool value)
        {
            isPaused = value;
        }

        private void NetworkSessionManager(GameTime gameTime)
        {
            if (networkSession == null)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.F1))
                    CreateSession();
                if (Keyboard.GetState().IsKeyDown(Keys.F2))
                    JoinSession();
            }
            else
            {
                // If we are in a network session, update it.
                UpdateNetworkSession(gameTime);
            }
        }

        private bool IsPlayerLogged()
        {
            if (Gamer.SignedInGamers.Count == 0)
            {
                // If there are no profiles signed in, we cannot proceed.
                // Show the Guide so the user can sign in.
                if (!Guide.IsVisible)
                {
                    Guide.ShowSignIn(maxLocalGamers, false);
                    return false;
                }
            }
            return true;
        }



        private void JoinSession()
        {
            if (IsPlayerLogged())
            {
                DrawMessage("Joining session...");

                try
                {
                    // Search for sessions.
                    using (AvailableNetworkSessionCollection availableSessions =
                                NetworkSession.Find(NetworkSessionType.SystemLink,
                                                    maxLocalGamers, null))
                    {
                        if (availableSessions.Count == 0)
                        {
                            errorMessage = "No network sessions found.";
                            return;
                        }

                        // Join the first session we found.
                        networkSession = NetworkSession.Join(availableSessions[0]);

                        HookSessionEvents();
                    }
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }
        }

        private void CreateSession()
        {
            if (IsPlayerLogged())
            {
                DrawMessage("Creating session...");

                try
                {
                    networkSession = NetworkSession.Create(NetworkSessionType.SystemLink,
                                                           maxLocalGamers, maxGamers);

                    HookSessionEvents();
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }
        }

        private void HookSessionEvents()
        {
            networkSession.GamerJoined += new EventHandler<GamerJoinedEventArgs>(networkSession_GamerJoined);
            networkSession.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(networkSession_SessionEnded);
        }

        void networkSession_SessionEnded(object sender, NetworkSessionEndedEventArgs e)
        {
            errorMessage = e.EndReason.ToString();

            networkSession.Dispose();
            networkSession = null;
        }

        void networkSession_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            //TODO
            e.Gamer.Tag = new NetworkedErasme(this, viewportRect, e.Gamer.Gamertag);
        }

        private void UpdateNetworkSession(GameTime gameTime)
        {
            // Read inputs for locally controlled tanks, and send them to the server.
            foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
            {
                UpdateLocalGamer(gamer, gameTime);
            }

            // If we are the server, update all the tanks and transmit
            // their latest positions back out over the network.
            if (networkSession.IsHost)
            {
                UpdateServer(gameTime);
            }

            // Pump the underlying session object.
            networkSession.Update();

            // Make sure the session has not ended.
            if (networkSession == null)
                return;

            // Read any incoming network packets.
            foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
            {
                if (gamer.IsHost)
                {
                    ServerReadInputFromClients(gamer);
                }
                else
                {
                    ClientReadGameStateFromServer(gamer);
                }
            }
        }

        private void ClientReadGameStateFromServer(LocalNetworkGamer gamer)
        {
            // Keep reading as long as incoming packets are available.
            while (gamer.IsDataAvailable)
            {
                NetworkGamer sender;

                // Read a single packet from the network.
                gamer.ReceiveData(packetReader, out sender);

                // This packet contains data about all the players in the session.
                // We keep reading from it until we have processed all the data.
                while (packetReader.Position < packetReader.Length)
                {
                    // Read the state of one tank from the network packet.
                    byte gamerId = packetReader.ReadByte();
                    Vector2 position = packetReader.ReadVector2();
                    float rotation = (float)packetReader.ReadDouble();
                    // Look up which gamer this state refers to.
                    NetworkGamer remoteGamer = networkSession.FindGamerById(gamerId);

                    // This might come back null if the gamer left the session after
                    // the host sent the packet but before we received it. If that
                    // happens, we just ignore the data for this gamer.
                    if (remoteGamer != null)
                    {
                        // Update our local state with data from the network packet.
                        NetworkedErasme remoteErasme = remoteGamer.Tag as NetworkedErasme;

                        remoteErasme.Position = position;
                        remoteErasme.Rotation = rotation;
                    }
                }
            }
        }

        private void ServerReadInputFromClients(LocalNetworkGamer gamer)
        {
            // Keep reading as long as incoming packets are available.
            while (gamer.IsDataAvailable)
            {
                NetworkGamer sender;

                // Read a single packet from the network.
                gamer.ReceiveData(packetReader, out sender);

                if (!sender.IsLocal)
                {
                    // Look up the tank associated with whoever sent this packet.
                    NetworkedErasme remoteErasme = sender.Tag as NetworkedErasme;

                    // Read the latest inputs controlling this tank.
                    remoteErasme.Position = packetReader.ReadVector2();
                    remoteErasme.Rotation = (float)packetReader.ReadDouble();
                }
                else
                {
                    // Look up the tank associated with whoever sent this packet.
                    NetworkedErasme remoteErasme = sender.Tag as NetworkedErasme;

                    // Read the latest inputs controlling this tank.
                    remoteErasme.Position = playerManager.premierJoueur.erasme.Position;
                    remoteErasme.Rotation = playerManager.premierJoueur.erasme.Rotation;
                }
            }
        }

        private void UpdateServer(GameTime gameTime)
        {
            // Loop over all the players in the session, not just the local ones!
            foreach (NetworkGamer gamer in networkSession.AllGamers)
            {
                // Look up what erasme is associated with this player.
                NetworkedErasme erasme = gamer.Tag as NetworkedErasme;

                // Update the erasme.
                erasme.Update(gameTime);

                // Write the erasme state into the output network packet.
                packetWriter.Write(gamer.Id);
                packetWriter.Write(erasme.Position);
                packetWriter.Write((double)erasme.Rotation);
            }

            // Send the combined data for all tanks to everyone in the session.
            LocalNetworkGamer server = (LocalNetworkGamer)networkSession.Host;

            server.SendData(packetWriter, SendDataOptions.InOrder);
        }

        private void UpdateLocalGamer(LocalNetworkGamer gamer, GameTime gameTime)
        {
            //TODO

            // Only send if we are not the server. There is no point sending packets
            // to ourselves, because we already know what they will contain!
            if (!networkSession.IsHost)
            {
                // Write our latest input state into a network packet.
                packetWriter.Write(playerManager.premierJoueur.erasme.Position);
                packetWriter.Write((double)playerManager.premierJoueur.erasme.Rotation);

                // Send our input data to the server.
                gamer.SendData(packetWriter,
                               SendDataOptions.InOrder, networkSession.Host);
            }
            foreach (NetworkGamer nGamer in networkSession.AllGamers)
            {
                // Look up what erasme is associated with this player.
                NetworkedErasme erasme = gamer.Tag as NetworkedErasme;

                // Update the erasme.
                erasme.Update(gameTime);
            }
        }

        private void PauseManager(GameTime gameTime)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.Pause) &&
                previousKeyboardState.IsKeyUp(Keys.Pause)) ||
                (Keyboard.GetState().IsKeyDown(Keys.P) &&
                previousKeyboardState.IsKeyUp(Keys.P)))
            {
                isPaused = !isPaused;
            }
            else if (Guide.IsVisible)
            {
                isPaused = true;
                isPausedByGuide = true;
            }
            else if (!Guide.IsVisible)
            {
                if (isPausedByGuide)
                {
                    isPausedByGuide = false;
                    isPaused = false;
                }
            }

            previousKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //init
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);

            //dessin des  objets du jeu
            decorsManager.Draw(gameTime, spriteBatch);
            //networked players 
            DrawNetworkedPlayers(gameTime);
            playerManager.Draw(gameTime, spriteBatch);
            mechantManager.Draw(gameTime, spriteBatch);
            explosionManager.Draw(gameTime, spriteBatch);
            xpManager.Draw(gameTime, spriteBatch);
            lifeManager.Draw(spriteBatch);

            //gestion de la pause
            if (isPaused)
            {
                spriteBatch.Draw(pauseImage.Sprite, pauseImage.Position, null, Color.White, pauseImage.Rotation, Vector2.Zero, pauseImage.Scale, SpriteEffects.None, 0);
            } 

            //affichage des erreurs reseau
            spriteBatch.DrawString(networkFont, errorMessage, new Vector2(20, 200), Color.Red);

            //affiche la bienté reseau
            if (networkSession != null)
            {
                string temp = "";
                if (networkSession.IsHost)
                {
                    temp = "Mode Serveur.";
                }
                else
                {
                    temp = "Mode Client. Connecté"; ;
                }
                spriteBatch.DrawString(networkFont, temp, new Vector2(20, 550), Color.Violet);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawNetworkedPlayers(GameTime gameTime)
        {
            if (networkSession != null)
            {
                string gamerTag = "";
                Color gamerTagColor = Color.Black;
                Vector2 gamerTagPosition = new Vector2(600, 580);

                // For each person in the session...
                foreach (NetworkGamer gamer in networkSession.AllGamers)
                {
                    
                    // Look up the tank object belonging to this network gamer.
                    NetworkedErasme remoteErasme = gamer.Tag as NetworkedErasme;

                    // Draw the tank.
                    if (!gamer.IsLocal)
                    {
                        remoteErasme.Update(gameTime);
                        remoteErasme.Draw(gameTime, spriteBatch);
                    }

                    // Draw a gamertag label.
                    gamerTag = gamer.Gamertag;
                    gamerTagPosition = new Vector2(gamerTagPosition.X, gamerTagPosition.Y - 20);
                    gamerTagColor = Color.Black;

                    if (gamer.IsHost)
                        gamerTag += " (server)";

                    // Flash the gamertag to yellow when the player is talking.
                    if (gamer.IsTalking)
                        gamerTagColor = Color.Yellow;

                    spriteBatch.DrawString(networkFont, gamerTag, gamerTagPosition, gamerTagColor, 0,
                                           Vector2.Zero, 1, SpriteEffects.None, 0);
                
                }
            }
            
        }

        void DrawMessage(string message)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(networkFont, message, new Vector2(20, 170), Color.Black);
            spriteBatch.End();
        }
    }
}
