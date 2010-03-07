using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme.GameObjects
{
    public class BuloManager
    {
        public BuloState buloState { get; set; }
        public float buloPorteeMax;
        public GameObject bulo;

        public ErasmeManager erasmeManager;

        public ExplosionManager explosionManager;

        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
#if !XBOX
        KeyboardState previousKeyboardState = Keyboard.GetState();
#endif

        public BuloManager(UltimateErasme game, ErasmeManager erasmeManager)
        {
            bulo = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Bulo\bulo"));
            this.erasmeManager = erasmeManager;
            buloState = BuloState.pasSorti;

            explosionManager = new ExplosionManager(game, erasmeManager);
        }

        public void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed &&
                previousGamePadState.Buttons.RightShoulder == ButtonState.Released)
            {
                RentrerSortirBulo();
            }
            if (buloState == BuloState.sorti)
            {
                if (gamePadState.Buttons.B == ButtonState.Pressed &&
                    previousGamePadState.Buttons.B == ButtonState.Released)
                {
                    LancerBulorang();
                }
            }
            else if (buloState == BuloState.debutLance || buloState == BuloState.retourLance)
            {
                if (gamePadState.Buttons.B == ButtonState.Pressed &&
                    previousGamePadState.Buttons.B == ButtonState.Released)
                {
                    explosion();
                }
            }
            previousGamePadState = gamePadState;

#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.B) &&
               previousKeyboardState.IsKeyUp(Keys.B))
            {
                RentrerSortirBulo();
            }
            if (buloState == BuloState.sorti)
            {
                if (keyboardState.IsKeyDown(Keys.Z) &&
                previousKeyboardState.IsKeyUp(Keys.Z))
                {
                    LancerBulorang();
                }
            }
            else if (buloState == BuloState.debutLance || buloState == BuloState.retourLance)
            {
                if (keyboardState.IsKeyDown(Keys.Z) &&
                previousKeyboardState.IsKeyUp(Keys.Z))
                {
                    explosion();
                }
            }
            previousKeyboardState = keyboardState;
#endif

            BuloUpdate();
        }

        private void LancerBulorang()
        {
            buloState = BuloState.debutLance;
            buloPorteeMax = erasmeManager.erasme.Position.X + 400;
            erasmeManager.soundManager.Plop();
        }

        private void RentrerSortirBulo()
        {
            if (buloState == BuloState.pasSorti)
            {
                buloState = BuloState.sorti;
                erasmeManager.soundManager.BuloBulo();
                bulo.Alive = true;
            }
            else if (buloState == BuloState.sorti)
            {
                buloState = BuloState.pasSorti;
                bulo.Alive = false;
                buloPorteeMax = 0;
            }
        }

        private void explosion()
        {
            explosionManager.NouvelleExplosion(bulo.Position);
            bulo.Alive = false;
            buloState = BuloState.pasSorti;
        }




        //gére le bulo
        private void BuloUpdate()
        {
            if (buloState != BuloState.pasSorti)
            {
                if (buloState == BuloState.sorti)
                {
                    if (erasmeManager.erasme.Sprite == erasmeManager.jumpManager.erasmeMonte ||
                        erasmeManager.erasme.Sprite == erasmeManager.jumpManager.voltaireMonte)
                    {
                        bulo.Position = erasmeManager.erasme.Position + new Vector2(30, 100);
                    }
                    else if (erasmeManager.erasme.Sprite == erasmeManager.jumpManager.erasmeDescend ||
                        erasmeManager.erasme.Sprite == erasmeManager.jumpManager.voltaireDescend)
                    {
                        bulo.Position = erasmeManager.erasme.Position + new Vector2(20, -50);
                    }
                    else
                    {
                        bulo.Position = erasmeManager.erasme.Position + (new Vector2(100, 35));
                    }
                    bulo.Rotation = erasmeManager.erasme.Rotation;
                }

                else if (buloState == BuloState.debutLance)
                {
                    if (bulo.Position.X < buloPorteeMax)
                    {
                        bulo.Position += new Vector2(5, 0);
                        bulo.Rotation += 0.5f;
                    }
                    else
                    {
                        buloState = BuloState.retourLance;
                    }
                }
                else if (buloState == BuloState.retourLance)
                {

                    if (bulo.Position.Y > erasmeManager.erasme.Position.Y)
                    {
                        bulo.Position -= new Vector2(0, 1);
                        bulo.Rotation += 0.5f;
                    }
                    else if (bulo.Position.Y < erasmeManager.erasme.Position.Y)
                    {
                        bulo.Position += new Vector2(0, 1);
                        bulo.Rotation += 0.5f;
                    }
                    if (bulo.Position.X > erasmeManager.erasme.Position.X)
                    {
                        bulo.Position -= new Vector2(5, 0);
                        bulo.Rotation += 0.5f;
                    }
                    else
                    {
                        buloState = BuloState.sorti;
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (bulo.Alive)
            {
                spriteBatch.Draw(bulo.Sprite, bulo.Position, null, Color.White, bulo.Rotation, bulo.Center, bulo.Scale, SpriteEffects.None, 0);
            }
        }
    }
}
