using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace UltimateErasme.MenuState
{
    class PauseMenuState : GameState
    {
        GraphicsDeviceManager graphics;
        Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;
        List<ButtonMenu> buttonMenu;
        GameObject MousePointer;
        private static PauseMenuState instancePMS;
        Rectangle viewportRect;

        private PauseMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;

            buttonMenu = new List<ButtonMenu>();
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
            MousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));

            viewportRect = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);

            ButtonMenu bouton = new ButtonMenu("Continuer", Color.Black, Color.DarkGreen, new Vector2((4 * viewportRect.Width) / (float)10, (6 * viewportRect.Height) / (float)16));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Sauvegarder la partie", Color.Black, Color.DarkGreen, new Vector2((4 * viewportRect.Width) / (float)10, (7 * viewportRect.Height) / (float)16));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Charger une partie", Color.Black, Color.DarkGreen, new Vector2((4 * viewportRect.Width) / (float)10, (8 * viewportRect.Height) / (float)16));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Quitter et retourner au menu principal", Color.Black, Color.DarkGreen, new Vector2((4 * viewportRect.Width) / (float)10, (9 * viewportRect.Height) / (float)16));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Quitter le jeu", Color.Black, Color.DarkGreen, new Vector2((4 * viewportRect.Width) / (float)10, (10 * viewportRect.Height) / (float)16));
            buttonMenu.Add(bouton);
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
            foreach (ButtonMenu button in buttonMenu)
            {
                if (button.isPressed())
                {
                    if (button.getText().Equals("Quitter le jeu"))
                    {
                        game.Exit();
                    }
                    else if (button.getText().Equals("Continuer"))
                    {
                        MustChangeState(UltimateErasme.getInstance(game, graphics));
                    }
                    else if (button.getText().Equals("Sauvegarder la partie"))
                    {
                    }
                    else if (button.getText().Equals("Charger une partie"))
                    {
                    }
                    else if (button.getText().Equals("Quitter et retourner au menu principal"))
                    {
                        MustChangeState(MainMenuState.getInstance(game, graphics));
                    }
                }
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
            UltimateErasme.getInstance(game, graphics).Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            foreach (ButtonMenu button in buttonMenu)
            {
                if (button.isNear())
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getOnClickColor());
                }
                else
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getColor());
                }
            }
            spriteBatch.Draw(MousePointer.Sprite, MousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public override void MustChangeState(GameState futureState)
        {
            Thread.Sleep(300);
            game.currentState = futureState;
        }
    }
}
