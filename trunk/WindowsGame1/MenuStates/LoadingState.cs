using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace UltimateErasme.MenuStates
{
    class LoadingState : GameState
    {
        GraphicsDeviceManager graphics;
        Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;
        GameObject background;
        GameObject MousePointer;
        private static LoadingState instanceLS;
        Rectangle viewportRect;
        GameState nextState;
        int chargement;
        Random random = new Random();
        String[] loadingMessages;
        String loadingMessage;

        private LoadingState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            loadingMessages = new String[] {"Si vous êtes mouleux, pensez à cliquer sur recommandé lors du choix de vos caractéristiques.",
                "Pour mieux passer dans de petits espaces, pensez à vous vider de votre graisse avant.",
                "Si un gacheur vous propose de vous rejoindre, n'acceptez pas."};
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics, GameState nextState, int chargement) {
            if (instanceLS == null)
            {
                instanceLS = new LoadingState(game, graphics);
            }
            instanceLS.nextState = nextState;
            instanceLS.chargement = chargement;
            instanceLS.loadingMessage = instanceLS.loadingMessages[instanceLS.random.Next(instanceLS.loadingMessages.Length)];
            return instanceLS;
        }
         /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor2"));
            MousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));

            viewportRect = new Rectangle(game.GraphicsDevice.Viewport.Width / 4, game.GraphicsDevice.Viewport.Height * 14 / 20, game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height * 6 / 20);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (chargement <= 0)
            {
                MustChangeState(nextState);
            }
            chargement--;
            //Thread.Sleep(300);
            MousePointer.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            //init
            game.GraphicsDevice.Clear(Color.Red);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Rectangle viewportRectBack = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(background.Sprite, viewportRectBack, Color.White);

            float y;
            if (chargement % 80 < 20)
            {
                y = ErasmeUtils.afficherTexte("Chargement", game, viewportRect, spriteBatch, font, Color.DarkBlue, viewportRect.Y);
            }
            else if (chargement % 80 >= 20 && chargement % 80 < 40)
            {
                y = ErasmeUtils.afficherTexte("Chargement ...", game, viewportRect, spriteBatch, font, Color.DarkBlue, viewportRect.Y);
            }
            else if (chargement % 80 >= 40 && chargement % 80 < 60)
            {
                y = ErasmeUtils.afficherTexte("Chargement ..", game, viewportRect, spriteBatch, font, Color.DarkBlue, viewportRect.Y);
            }
            else
            {
                y = ErasmeUtils.afficherTexte("Chargement .", game, viewportRect, spriteBatch, font, Color.DarkBlue, viewportRect.Y);
            }
            ErasmeUtils.afficherTexte("Astuce : " + loadingMessage, game, viewportRect, spriteBatch, font, Color.DarkBlue, y + 50);

            spriteBatch.Draw(MousePointer.Sprite, MousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public override void MustChangeState(GameState futureState)
        {
            game.currentState = futureState;
            game.currentState.LoadContent();
        }
    }
}
