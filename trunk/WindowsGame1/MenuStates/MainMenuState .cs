using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.MenuStates;
using System.Threading;
using UltimateErasme.InputTesters;

namespace UltimateErasme.MenuState
{
    class MainMenuState : GameState
    {
        public GraphicsDeviceManager graphics;
        public Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;

        List<ButtonMenu> buttonMenu;
        GameObject background;
        GameObject MousePointer;
        private static MainMenuState instanceMMS;
        KeyboardTester keyboardTester;

        private MainMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            
            buttonMenu = new List<ButtonMenu>();
            ButtonMenu bouton = new ButtonMenu("Jouer", Color.Red, Color.DarkGreen, new Vector2(300, 350));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Créer son Personnage", Color.Red, Color.DarkGreen, new Vector2(300, 400));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Options", Color.Red, Color.DarkGreen, new Vector2(300, 450));
            buttonMenu.Add(bouton);
            bouton = new ButtonMenu("Quitter", Color.Red, Color.DarkGreen, new Vector2(300, 500));
            buttonMenu.Add(bouton);

            keyboardTester = new KeyboardTester();
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics) {
            if (instanceMMS == null)
            {
                instanceMMS = new MainMenuState(game, graphics);
            }
            return instanceMMS;
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
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Menu\titre"));
            MousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));
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
                    if (button.getText().Equals("Quitter"))
                    {
                        game.Exit();
                    }
                    else if (button.getText().Equals("Créer son Personnage"))
                    {
                        MustChangeState(CreatePersoMenuState.getInstance(game, graphics));
                    }
                    else if (button.getText().Equals("Options")){
                        MustChangeState(OptionsMenuState.getInstance(game, graphics));
                    }
                    else
                    {
                        MustChangeState(UltimateErasme.getInstance(game, graphics));
                    }
                }
            }
             
            keyboardTester.GetKeyboard();

            if (keyboardTester.test(Keys.F))
                graphics.ToggleFullScreen();

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
            Rectangle viewportRect = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
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
            game.currentState.LoadContent();
        }
    }
}
