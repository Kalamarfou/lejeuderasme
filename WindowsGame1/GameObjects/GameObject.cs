using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace UltimateErasme.GameObjects
{
    class GameObject
    {
        public Texture2D Sprite { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Velocity { get; set; }
        public bool Alive { get; set; }
        public float Scale { get; set; }

        public GameObject(Texture2D loadedTexture)
        {
            Rotation = 0.0f;
            Position = Vector2.Zero;
            Sprite = loadedTexture;
            Center = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            Velocity = Vector2.Zero;
            Alive = false;
            Scale = 1;
        }
    }
}
