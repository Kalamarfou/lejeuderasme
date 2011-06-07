using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using UltimateErasme.InputTesters;

namespace UltimateErasme.MenuStates
{
    class EntryMessagesState : GameState
    {
        GraphicsDeviceManager graphics;
        Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;
        List<ButtonMenu> buttonMenu;
        GameObject mousePointer;
        private static EntryMessagesState instanceEMS;
        Rectangle viewportRect;
        private string entry;
        bool toucheEnfoncee = false;
        int max = 20;
        KeyboardTester keyboardTester;

        private EntryMessagesState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            keyboardTester = new KeyboardTester();
            buttonMenu = new List<ButtonMenu>();
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics) {
            if (instanceEMS == null)
            {
                instanceEMS = new EntryMessagesState(game, graphics);
            }
            return instanceEMS;
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
            mousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));

            viewportRect = new Rectangle(game.GraphicsDevice.Viewport.Width / 3, game.GraphicsDevice.Viewport.Height * 9 / 20, game.GraphicsDevice.Viewport.Width / 3, game.GraphicsDevice.Viewport.Height / 10);
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
            entry = ErasmeUtils.gestionClavier(graphics, mousePointer, viewportRect, entry, max, toucheEnfoncee, out toucheEnfoncee);

            keyboardTester.GetKeyboard();

            if (keyboardTester.test(Keys.Enter))
            {
                MustChangeState(UltimateErasme.getInstance(game, graphics));
            }

            mousePointer.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
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

            ErasmeUtils.afficherTexte("Message : " + entry, game, viewportRect, spriteBatch, font, Color.DarkBlue, viewportRect.Y);
            spriteBatch.Draw(mousePointer.Sprite, mousePointer.Position, Color.White);
            spriteBatch.End();
        }

        public override void MustChangeState(GameState futureState)
        {
            Thread.Sleep(300);
            game.currentState = futureState;
        }
    }
}
