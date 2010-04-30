using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;

namespace UltimateErasme.Network
{
    class NetworkedErasme
    {

        public Vector2 Position { get; set; }

        public GameObject erasme;

        public NetworkedErasme(UltimateErasme game)
        {
            erasme = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme"));
        }


        internal void Update()
        {
            erasme.Position = Position;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(erasme.Sprite, erasme.Position, null, Color.White, erasme.Rotation, erasme.Center, erasme.Scale, SpriteEffects.None, 0);
        }
    }
}
