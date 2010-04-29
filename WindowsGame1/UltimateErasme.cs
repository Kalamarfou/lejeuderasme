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
        static public XpManager xpManager;

        bool isPaused = false;
        GameObject pauseImage;
        KeyboardState previousKeyboardState = Keyboard.GetState();

        public UltimateErasme()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            xpManager = new XpManager(this);

            pauseImage = new GameObject(this.Content.Load<Texture2D>(@"Sprites\Pause\Pause"));
            pauseImage.Position = new Vector2(0, 80);
            
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

            PauseManager(gameTime);
            

            if (!isPaused)
            {
                collisionsManager.Update(gameTime);
                decorsManager.Update(gameTime);
                playerManager.Update(gameTime);
                mechantManager.Update(gameTime);
                explosionManager.Update(gameTime);
                xpManager.Update(gameTime);
            }

            base.Update(gameTime);
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

            previousKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            decorsManager.Draw(gameTime, spriteBatch);
            playerManager.Draw(gameTime, spriteBatch);
            mechantManager.Draw(gameTime, spriteBatch);
            explosionManager.Draw(gameTime, spriteBatch);
            xpManager.Draw(gameTime, spriteBatch);
            if (isPaused)
            {
                spriteBatch.Draw(pauseImage.Sprite, pauseImage.Position, null, Color.White, pauseImage.Rotation, Vector2.Zero, pauseImage.Scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
