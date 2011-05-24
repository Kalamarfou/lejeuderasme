using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UltimateErasme.GameObjects
{
    public class ErasmeAccessoiresCollection : ArrayList 
    {
        public void Update(GameTime gameTime, Vector2 position, float rotation)
        {
            foreach (ErasmeAccessoire item in this)
            {
                item.Update(gameTime, position, rotation);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (ErasmeAccessoire item in this)
            {
                item.Draw(gameTime, spriteBatch);
            }
        }

        public int AddDirectFromTexture(Texture2D accessoire)
        {
            return base.Add(new ErasmeAccessoire(new GameObject(accessoire)));
        }

        public void SetAllVisible(Boolean visible)
        {
            foreach (ErasmeAccessoire item in this)
            {
                item.IsVisible = visible;
            }
        }
    }
}
