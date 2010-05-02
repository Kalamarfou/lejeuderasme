using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;

namespace UltimateErasme.Network
{
    class NetworkedErasme : ErasmeManager
    {

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        public NetworkedErasme(UltimateErasme game, Rectangle viewPortRect) : 
            base(game,viewPortRect)
        {
        }


        public override void Update(GameTime gameTime)
        {
            erasme.Position = Position;
            erasme.Rotation = Rotation;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(erasme.Sprite, erasme.Position, null, Color.White, erasme.Rotation, erasme.Center, erasme.Scale, SpriteEffects.None, 0);
        }
    }
}
