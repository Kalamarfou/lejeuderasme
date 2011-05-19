using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme
{
    class PauseMenuState : GameState
    {
        GraphicsDeviceManager graphics;
        Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;
        List<String> text = new List<String>() { "Continuer", "Quitter" };
        Vector2 position;
        GameObject background;
        GameObject MousePointer;
        private static PauseMenuState instancePMS;

        private PauseMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics) {
            if (instancePMS == null)
            {
                instancePMS = new PauseMenuState(game, graphics);
            }
            return instancePMS;
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
            // Bouh, pas beau, buggué
            int x = 300;
            int y = 150;
            foreach (String textMenu in text)
            {
                if ((Mouse.GetState().LeftButton == ButtonState.Pressed)
                    && (Math.Abs(Mouse.GetState().X - x) < 80)
                    && (Math.Abs(Mouse.GetState().Y - y) < 25))
                {
                    if (textMenu.Equals("Quitter"))
                    {
                        game.Exit();
                    }
                    else
                    {
                        MustChangeState(UltimateErasme.getInstance(game, graphics));
                    }
                }
                y += 50;
            }

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
            int x = 300;
            int y = 150;
            Rectangle viewportRect = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
            //beurk, c'est moche (et ça marche moyen ..), il faudrait trouver un autre moyen (moi je créerais une classe MenuItem, et chaque item vivrait sa vie)
            foreach (String textMenu in text)
            {
                if ((Math.Abs(Mouse.GetState().X - x) < 80) && (Math.Abs(Mouse.GetState().Y - y) < 25))
                {
                    spriteBatch.DrawString(font, textMenu, new Vector2(x, y), Color.DarkGreen);
                }
                else
                {
                    spriteBatch.DrawString(font, textMenu, new Vector2(x, y), Color.Black);
                }
                y += 50;
            }
            spriteBatch.Draw(MousePointer.Sprite, MousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public override void MustChangeState(GameState futureState)
        {
            game.currentState = futureState;
        }
    }
}
