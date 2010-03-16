using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects.enums;

namespace UltimateErasme.GameObjects
{
    public class JumpManager
    {

        public ErasmeManager erasmeManager;

        public JumpState jumpState;
        public int hauteurDuSol;
        public Vector2 jumpVelocity;
        string sensDuDoubleSaut = "";

        public Texture2D erasmeMonte, erasmeDescend;
        public Texture2D voltaireMonte, voltaireDescend;

        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
#if !XBOX
        KeyboardState previousKeyboardState = Keyboard.GetState();
#endif

        public JumpManager( UltimateErasme game, ErasmeManager erasmeManager)
        {
            this.erasmeManager = erasmeManager;
            erasmeMonte = game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme_blup");
            erasmeDescend = game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme_no_blup");
            voltaireMonte = game.Content.Load<Texture2D>(@"Sprites\Characters\Voltaire\voltaire_blup");
            voltaireDescend = game.Content.Load<Texture2D>(@"Sprites\Characters\Voltaire\voltaire_no_blup");
            hauteurDuSol = erasmeManager.viewportRect.Bottom - erasmeManager.erasme.Sprite.Height / 2 - 100;
            erasmeManager.erasme.Position = new Vector2(erasmeManager.erasme.Sprite.Width / 2, hauteurDuSol);
            jumpVelocity = new Vector2(0, 4);
            jumpState = JumpState.auSol;
        }

        //TODO
        public void Update(GameTime gametime, ControllerType controllerType)
        {
            if (!(controllerType == ControllerType.keyboard))
            {
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                if (controllerType == ControllerType.xBoxControler2)
                {
                    gamePadState = GamePad.GetState(PlayerIndex.Two);
                }
                //vrai update des boutons
                UpdateXboxControler(gametime, gamePadState);
                previousGamePadState = gamePadState;
            }

#if !XBOX
            if (controllerType == ControllerType.keyboard ||
               controllerType == ControllerType.keyboardPlusXBoxControler1)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                //vrai update des boutons
                UpdateKeyBoard(gametime, keyboardState);
                previousKeyboardState = keyboardState;
            }
#endif

            JumpUpdate();
        }

        private void UpdateKeyBoard(GameTime gametime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space) &&
                    previousKeyboardState.IsKeyUp(Keys.Space) &&
                    jumpState == JumpState.auSol)
            {
                Sauter();
            }
            else if (keyboardState.IsKeyDown(Keys.Space) &&
                    previousKeyboardState.IsKeyUp(Keys.Space) &&
                    jumpState == JumpState.arriveEnHaut)
            {
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    DoubleSauter("Left");
                }
                else
                {
                    DoubleSauter("Right");
                }

            }
        }

        private void UpdateXboxControler(GameTime gametime, GamePadState gamePadState)
        {
            if (gamePadState.Buttons.A == ButtonState.Pressed &&
                    previousGamePadState.Buttons.A == ButtonState.Released &&
                    jumpState == JumpState.auSol)
            {
                Sauter();
            }

            else if (gamePadState.Buttons.A == ButtonState.Pressed &&
                    previousGamePadState.Buttons.A == ButtonState.Released
                    && jumpState == JumpState.arriveEnHaut)
            {
                if (gamePadState.ThumbSticks.Left.X < 0)
                {
                    DoubleSauter("Left");
                }
                else
                {
                    DoubleSauter("Right");
                }
            }
        }

        private void DoubleSauter(string sens)
        {
            jumpState = JumpState.doubleDecollage;
            sensDuDoubleSaut = sens;
            if (erasmeManager.attackManager.attackState == AttackState.pasAttaque)
            {
                if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.erasme)
                {
                    erasmeManager.erasme.Sprite = erasmeManager.erasmeNormal;
                }
                else if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.voltaire)
                {
                    erasmeManager.erasme.Sprite = erasmeManager.voltaireNormal;
                }
            }
            erasmeManager.soundManager.DoubleSaut();
        }

        private void Sauter()
        {
            jumpState = JumpState.decollage;
            if (erasmeManager.attackManager.attackState == AttackState.pasAttaque)
            {
                if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.voltaire)
                {
                    erasmeManager.erasme.Sprite = voltaireMonte ;
                }
                else if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.erasme)
                {
                    erasmeManager.erasme.Sprite = erasmeMonte;
                }
            }
            erasmeManager.soundManager.Saut();
        }

        public void JumpUpdate()
        {
            switch (jumpState)
            {
                case JumpState.auSol:
                    break;
                case JumpState.decollage:
                    JumpDecollage();
                    break;
                case JumpState.arriveEnHaut:
                    JumpArriveEnHaut();
                    break;
                case JumpState.toutEnHaut:
                    JumpToutEnHaut();
                    break;
                case JumpState.repartEnBas:
                    JumpRepartEnBas();
                    break;
                case JumpState.atterissage:
                    JumpAtterissage();
                    break;
                case JumpState.doubleDecollage:
                    JumpDoubleDecollage();
                    break;
                case JumpState.doubleArriveEnHaut:
                    JumpDoubleArriveEnHaut();
                    break;
                case JumpState.doubleToutEnHaut:
                    JumpDoubleToutEnHaut();
                    break;
                case JumpState.doubleRepartEnBas:
                    JumpDoubleRepartEnBas();
                    break;
                case JumpState.doubleAtterissage:
                    JumpDoubleAtterissage();
                    break;
                default:
                    break;
            }
        }

        private void JumpAtterissage()
        {
            erasmeManager.erasme.Position += jumpVelocity;
            if (erasmeManager.erasme.Position.Y >= hauteurDuSol)
            {
                JumpTerminerSaut();
            }
        }

        private void JumpTerminerSaut()
        {
            jumpState = JumpState.auSol;
            if (erasmeManager.attackManager.attackState == AttackState.pasAttaque)
            {
                if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.voltaire)
                {
                    erasmeManager.erasme.Sprite = erasmeManager.voltaireNormal;
                }
                else if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.erasme)
                {
                    erasmeManager.erasme.Sprite = erasmeManager.erasmeNormal;
                }
            }
        }

        private void JumpRepartEnBas()
        {
            erasmeManager.erasme.Position += jumpVelocity / 2;
            if (erasmeManager.erasme.Position.Y >= hauteurDuSol - 80)
            {
                jumpState = JumpState.atterissage;
            }
        }

        private void JumpToutEnHaut()
        {
            jumpState = JumpState.repartEnBas;
            if (erasmeManager.attackManager.attackState == AttackState.pasAttaque)
            {
                if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.voltaire)
                {
                    erasmeManager.erasme.Sprite = voltaireDescend;
                }
                else if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.erasme)
                {
                    erasmeManager.erasme.Sprite = erasmeDescend;
                }
            }
        }

        private void JumpDoubleAtterissage()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            erasmeManager.erasme.Position += jumpVelocity;
            if (erasmeManager.erasme.Position.Y >= hauteurDuSol - 80)
            {
                jumpState = JumpState.atterissage;
                erasmeManager.erasme.Rotation = 0;
            }
        }

        private void JumpDoubleRepartEnBas()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            erasmeManager.erasme.Position += jumpVelocity / 2;
            if (erasmeManager.erasme.Position.Y >= hauteurDuSol - 180)
            {
                jumpState = JumpState.doubleAtterissage;
            }
        }

        private void JumpDoubleToutEnHaut()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            jumpState = JumpState.doubleRepartEnBas;
        }

        private void JumpDoubleArriveEnHaut()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            erasmeManager.erasme.Position -= jumpVelocity / 2;
            if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 200)
            {
                jumpState = JumpState.doubleToutEnHaut;
            }
        }

        private void JumpDoubleDecollage()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            erasmeManager.erasme.Position -= jumpVelocity;
            if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 180)
            {
                jumpState = JumpState.doubleArriveEnHaut;
            }
        }

        private void JumpArriveEnHaut()
        {
            erasmeManager.erasme.Position -= jumpVelocity / 2;
            if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 100)
            {
                jumpState = JumpState.toutEnHaut;
            }
        }

        private void JumpDecollage()
        {
            erasmeManager.erasme.Position -= jumpVelocity;
            if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 80)
            {
                jumpState = JumpState.arriveEnHaut;
            }
        }

        private void RotationDoubleSautManager(string sensDuDoubleSaut)
        {
            if (sensDuDoubleSaut == "Left")
            {
                erasmeManager.erasme.Rotation -= 0.2f;
            }
            else
            {
                erasmeManager.erasme.Rotation += 0.2f;
            }
        }

    }
}
