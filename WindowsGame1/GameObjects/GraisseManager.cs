using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UltimateErasme.GameObjects
{
    public class GraisseManager
    {
        public ErasmeManager erasmeManager;

        public Texture2D graisse;
        public ArrayList boulesDeGraisse = new ArrayList();

        public GraisseManager(UltimateErasme game, ErasmeManager erasmeManager)
        {
            this.erasmeManager = erasmeManager;
            graisse = game.Content.Load<Texture2D>(@"Sprites\Graisse\graisse");
        }

        public void Update(GameTime gametime)
        {
            GraisseManagerAnimation();
        }

        private void GraisseManagerAnimation()
        {
            if (boulesDeGraisse.Count > 0)
            {
                foreach (GameObject boule in boulesDeGraisse)
                {
                    boule.Position += boule.Velocity;
                }
                for (int i = boulesDeGraisse.Count - 1; i >= 0; i--)
                {
                    if (!erasmeManager.viewportRectPlus.Contains(new Point(
                        (int)((GameObject)boulesDeGraisse[i]).Position.X,
                        (int)((GameObject)boulesDeGraisse[i]).Position.Y)))
                    {
                        boulesDeGraisse.RemoveAt(i);
                    }
                }
            }

        }

        public void TirerBouleDeGraisse()
        {
            if (erasmeManager.erasme.Rotation == 0)
            {
                GameObject boule = new GameObject(graisse);

                boule.Velocity = new Vector2((float)Math.Cos(erasmeManager.erasme.Rotation) * 5f,
                            (float)Math.Sin(erasmeManager.erasme.Rotation) * 5f);

                boule.Position = erasmeManager.erasme.Position + new Vector2(30, 30);
                boule.Rotation = erasmeManager.erasme.Rotation;
                boulesDeGraisse.Add(boule);
                erasmeManager.soundManager.AttaqueErasme();
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    GameObject boule = new GameObject(graisse);
                    boule.Rotation = i * MathHelper.PiOver4;
                    boule.Velocity = new Vector2((float)Math.Cos(boule.Rotation) * 5f,
                                (float)Math.Sin(boule.Rotation) * 5f);

                    boule.Position = erasmeManager.erasme.Position + new Vector2(30, 30);
                    boulesDeGraisse.Add(boule);
                }
                erasmeManager.soundManager.AttaqueErasme360();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GameObject boule in boulesDeGraisse)
            {
                spriteBatch.Draw(boule.Sprite, boule.Position, null, Color.White, boule.Rotation, boule.Center, boule.Scale, SpriteEffects.None, 0);

            }
        }
    }
}
