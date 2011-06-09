using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;

namespace UltimateErasme.MenuStates
{
    class SavedPersoMenuState : GameState
    {
        public GraphicsDeviceManager graphics;
        public Game game;
        private static GameState instanceSPMS;
        SpriteBatch spriteBatch;
        SpriteFont font;
        GameObject background;
        GameObject MousePointer;

        private SavedPersoMenuState(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics)
        {
            if (instanceSPMS == null)
            {
                instanceSPMS = new SavedPersoMenuState(game, graphics);
            }
            return instanceSPMS;
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Menu\titre"));
            MousePointer = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Dialogues\graisseCursor"));
        }

        public override void UnloadContent()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public override void MustChangeState(GameState futureState)
        {
            Thread.Sleep(300);
            game.currentState = futureState;
            //game.currentState.LoadContent();
        }
    }
}
