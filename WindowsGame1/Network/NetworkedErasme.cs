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
        SpriteFont networkFont;
        string gamerTag;

        public NetworkedErasme(UltimateErasme game, Rectangle viewPortRect, string gamerTag) : 
            base(game,viewPortRect)
        {
            networkFont = game.Content.Load<SpriteFont>("Fonts/NetworkFont");
            this.gamerTag = gamerTag;
        }


        public override void Update(GameTime gameTime)
        {
            erasme.Position = Position;
            erasme.Rotation = Rotation;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            Vector2 temp = new Vector2(erasme.Position.X - 60,erasme.Position.Y - 160);
            spriteBatch.DrawString(networkFont, this.gamerTag, temp ,Color.Red);
            //spriteBatch.Draw(erasme.Sprite, erasme.Position, null, Color.Blue, erasme.Rotation, erasme.Center, erasme.Scale, SpriteEffects.None, 0);
        }
    }
}
