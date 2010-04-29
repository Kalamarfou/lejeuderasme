using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.GameObjects.enums;
using System.Collections;
using UltimateErasme.InputTesters;
using UltimateErasme.XP;

namespace UltimateErasme.GameObjects
{
    public class BuloManager
    {
        public BuloState buloState { get; set; }
        public float buloPorteeMax;
        public GameObject bulo;

        UltimateErasme game;
        ErasmeManager erasmeManager;

        GamePadTester gamePadTester = new GamePadTester();
#if !XBOX
        KeyboardTester keyboardTester = new KeyboardTester();
#endif

        public BuloManager(UltimateErasme game, ErasmeManager erasmeManager)
        {
            bulo = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Bulo\bulo"));
            this.game = game;
            this.erasmeManager = erasmeManager;
            buloState = BuloState.pasSorti;
        }

        //TODO
        public void Update(GameTime gameTime, ControllerType controllerType)
        {
            if (!(controllerType == ControllerType.keyboard))
            {
                gamePadTester.ChooseGamePad(controllerType);
                //vrai update des boutons
                UpdateXboxController(gameTime);
                gamePadTester.UpdatePreviousGamePadState();
            }

#if !XBOX
            if (controllerType == ControllerType.keyboard ||
               controllerType == ControllerType.keyboardPlusXBoxControler1)
            {
                keyboardTester.GetKeyboard();
                //vrai update des boutons
                UpdateKeyboard(gameTime);
                keyboardTester.UpdatePreviousKeyboardState();
            }
#endif

            BuloUpdate();
        }

        private void UpdateKeyboard(GameTime gameTime)
        {
            if (keyboardTester.test(Keys.B))
            {
                RentrerSortirBulo();
            }
            if (buloState == BuloState.sorti)
            {
                if (keyboardTester.test(Keys.Z))
                {
                    LancerBulorang();
                }
            }
            else if (buloState == BuloState.debutLance || buloState == BuloState.retourLance)
            {
                if (keyboardTester.test(Keys.Z))
                {
                    explosion(gameTime);
                }
            }
        }

        private void UpdateXboxController(GameTime gameTime)
        {
            if ( gamePadTester.test(Buttons.RightShoulder))
            {
                RentrerSortirBulo();
            }
            if (buloState == BuloState.sorti)
            {
                if (gamePadTester.test(Buttons.B))
                {
                    LancerBulorang();
                }
            }
            else if (buloState == BuloState.debutLance || buloState == BuloState.retourLance)
            {
                if (gamePadTester.test(Buttons.B))
                {
                    explosion(gameTime);
                }
            }
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
                UltimateErasme.xpManager.AddXp(XpEvents.SortageDeBulo);

                buloState = BuloState.sorti;
                erasmeManager.soundManager.BuloBulo();
                bulo.Alive = true;
            }
            else if (buloState == BuloState.sorti)
            {
                UltimateErasme.xpManager.AddXp(XpEvents.RentrageDeBulo);

                buloState = BuloState.pasSorti;
                bulo.Alive = false;
                buloPorteeMax = 0;
            }
        }

        private void explosion(GameTime gameTime)
        {
            if (UltimateErasme.xpManager.GetCurrentLevel() > 1)
            {
                ExplosionEnFonctionDuLevel(gameTime);
                bulo.Alive = false;
                buloState = BuloState.pasSorti;
            }
        }

        private void ExplosionEnFonctionDuLevel(GameTime gameTime)
        {
            if (UltimateErasme.xpManager.GetCurrentLevel() > 5)
            {
                game.explosionManager.NouvelleExplosion(bulo.Position, gameTime, ExplosionType.belle);
            }
            else if (UltimateErasme.xpManager.GetCurrentLevel() > 3)
            {
                game.explosionManager.NouvelleExplosion(bulo.Position, gameTime, ExplosionType.moyenBelle);
            }
            else
            {
                game.explosionManager.NouvelleExplosion(bulo.Position, gameTime, ExplosionType.moche);
            }
        }




        //gére le bulo
        private void BuloUpdate()
        {
            if (buloState != BuloState.pasSorti)
            {
                if (buloState == BuloState.sorti)
                {
                    BuloSortiUpdate();
                }

                else if (buloState == BuloState.debutLance)
                {
                    BuloLanceUpdate();
                }
                else if (buloState == BuloState.retourLance)
                {

                    BuloRetourLanceUpdate();
                }
            }
        }

        private void BuloRetourLanceUpdate()
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

        private void BuloLanceUpdate()
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

        private void BuloSortiUpdate()
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (bulo.Alive)
            {
                spriteBatch.Draw(bulo.Sprite, bulo.Position, null, Color.White, bulo.Rotation, bulo.Center, bulo.Scale, SpriteEffects.None, 0);
            }
        }

        internal void AjouterBuloAttaquesBox(ArrayList buloAttaquesBox)
        {
            if (buloState == BuloState.debutLance || buloState == BuloState.retourLance)
            {
                Rectangle rect = new Rectangle((int)bulo.Position.X, (int)bulo.Position.Y, bulo.Sprite.Width, bulo.Sprite.Height);
                buloAttaquesBox.Add(rect);
            }
        }
    }
}
