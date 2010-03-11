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
using UltimateErasme.GameObjects.enums;

namespace UltimateErasme.GameObjects
{
    class Mechant
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;

        public GameObject MechantGameObject { get; set; }
        public Texture2D MechantTexture { get; set; }

        public MechantState mechantState { get; set; }
        public MechantExplosionState mechantExplosionState { get; set; }
        public Vector2 mechantSpeed { get; set; }

        Color mechantColor = new Color();


        public Mechant(UltimateErasme game, Rectangle viewportRect, Vector2 position, Vector2 speed, float rotation, float scale, Color color)
        {
            this.viewportRect = viewportRect;
            viewportRectPlus = new Rectangle(viewportRect.X, viewportRect.Y, viewportRect.Width + 200, viewportRect.Height + 200);
            this.game = game;

            MechantTexture = game.Content.Load<Texture2D>(@"Sprites\Characters\Mechant\mechant");
            MechantGameObject = new GameObject(MechantTexture);
            MechantGameObject.Position = position;
            MechantGameObject.Rotation = rotation;
            MechantGameObject.Scale = scale;
            mechantColor = color;
            mechantSpeed = speed;
            mechantState = MechantState.normal;
            mechantExplosionState = MechantExplosionState.non;
        }

        public void Update(GameTime gameTime)
        {
            MechantGameObject.Position -= mechantSpeed;

            if (!viewportRectPlus.Contains(new Point(
                        (int)MechantGameObject.Position.X,
                        (int)MechantGameObject.Position.Y)))
            {
                mechantState = MechantState.mort;
            }

            if (mechantState == MechantState.enTrainDeMourrir)
            {
                MourrageAninationManager(gameTime);
            }

            if (mechantExplosionState == MechantExplosionState.explosionEnCours)
            {
                AttaqueAnimationManager(gameTime);
            }
        }

        private void MourrageAninationManager(GameTime gameTime)
        {
            //TODO
        }

        private void AttaqueAnimationManager(GameTime gameTime)
        {
            //TODO
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MechantGameObject.Sprite, MechantGameObject.Position, null, Color.White, 
                MechantGameObject.Rotation, MechantGameObject.Center, MechantGameObject.Scale, SpriteEffects.None, 0);
        }
    }
}
