using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UltimateErasme.GameObjects
{
    public class ErasmeAccessoire
    {
        public GameObject Accessoire { get; set; }
        public Boolean IsVisible { get; set; }

        public ErasmeAccessoire(GameObject accessoire)
        {
            this.Accessoire = accessoire;
            IsVisible = true;
        }

        public void Update(GameTime gameTime, Vector2 position, float rotation)
        {
            Accessoire.Position = position;
            Accessoire.Rotation = rotation;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(Accessoire.Sprite, Accessoire.Position, null, Color.White, Accessoire.Rotation, Accessoire.Center, Accessoire.Scale, SpriteEffects.None, 0);
            }
        }
    }
}
