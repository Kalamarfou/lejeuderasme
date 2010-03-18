using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections;
using UltimateErasme.ClassesDInternet.Particles;
using UltimateErasme.Sound;

namespace UltimateErasme.GameObjects
{
    public class DecorsManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;

        public GameObject background;

        public DecorsManager(UltimateErasme game, Rectangle viewportRect)
        {
            this.viewportRect = viewportRect;
            viewportRectPlus = new Rectangle(viewportRect.X, viewportRect.Y, viewportRect.Width + 100, viewportRect.Height + 100);
            this.game = game;
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor"));

        }


        public void Update(GameTime gameTime)
        {
            //TODO
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
        }
    }
}
