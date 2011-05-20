using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.MenuState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;

namespace UltimateErasme.MenuStates
{
    class OptionsMenuStatecs : GameState
    {
        public GraphicsDeviceManager graphics;
        public Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;

        List<ButtonMenu> buttonMenu;
        Vector2 position;
        GameObject background;
        GameObject MousePointer;
        private static OptionsMenuStatecs instanceOMS;

        private OptionsMenuStatecs(Game game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            
            buttonMenu = new List<ButtonMenu>();
            ButtonMenu bouton = new ButtonMenu("Flou", Color.Red, Color.DarkGreen, new Vector2(300, 350));
            buttonMenu.Add(bouton);
        }

        public static GameState getInstance(Game game, GraphicsDeviceManager graphics)
        {
            if (instanceOMS == null)
            {
                instanceOMS = new OptionsMenuStatecs(game, graphics);
            }
            return instanceOMS;
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void MustChangeState(GameState futuretate)
        {
            //game.currentState = futureState;
            game.currentState.LoadContent();
        }

    }
}
