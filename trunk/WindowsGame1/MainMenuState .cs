using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;

namespace UltimateErasme
{
    class MainMenuState : GameState
    {
        public GraphicsDeviceManager graphics;
        public Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;
        List<String> text;
        Vector2 position;
        GameObject background;

        public MainMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            text = new List<String>() {"Jouer", "Créer son Personnage", "Options", "Quitter" };
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
            font = game.Content.Load<SpriteFont>(@"Content\Fonts\XpFont");
            background = new GameObject(game.Content.Load<Texture2D>(@"Content\Sprites\Backgrounds\decor2"));
            position = new Vector2(100, 100);
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
            int x = 300;
            int y = 150;
            Rectangle viewportRect = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
            foreach (String textMenu in text)
            {
                spriteBatch.DrawString(font, textMenu, new Vector2(x, y), Color.Black);
                y += 50;
            }
            spriteBatch.End();
        }

        public override void MustChangeState(GameState futureState)
        {
            game.currentState = futureState;
        }
    }
}
