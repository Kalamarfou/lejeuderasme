using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects.enums;
using UltimateErasme.InputTesters;
using UltimateErasme.XP;

namespace UltimateErasme.GameObjects
{
    public class ErasmeJumpManager : JumpManager
    {

        public ErasmeManager erasmeManager;

        public Texture2D erasmeMonte, erasmeDescend;
        public Texture2D voltaireMonte, voltaireDescend;

        GamePadTester gamePadTester = new GamePadTester();
#if !XBOX
        KeyboardTester keyboardTester = new KeyboardTester();
#endif
        
        public ErasmeJumpManager( UltimateErasme game, ErasmeManager erasmeManager)
            :base()
        {
            this.erasmeManager = erasmeManager;
            erasmeMonte = game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme_blup");
            erasmeDescend = game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme_no_blup");
            voltaireMonte = game.Content.Load<Texture2D>(@"Sprites\Characters\Voltaire\voltaire_blup");
            voltaireDescend = game.Content.Load<Texture2D>(@"Sprites\Characters\Voltaire\voltaire_no_blup");
            hauteurDuSol = erasmeManager.viewportRect.Bottom - erasmeManager.erasme.Sprite.Height / 2 - 100;
            erasmeManager.erasme.Position = new Vector2(erasmeManager.erasme.Sprite.Width / 2, hauteurDuSol);
            
        }

        //TODO
        public void Update(GameTime gametime, ControllerType controllerType)
        {
            if (!(controllerType == ControllerType.keyboard))
            {
                gamePadTester.ChooseGamePad(controllerType);
                //vrai update des boutons
                UpdateXboxControler(gametime);
                gamePadTester.UpdatePreviousGamePadState();
            }

#if !XBOX
            if (controllerType == ControllerType.keyboard ||
               controllerType == ControllerType.keyboardPlusXBoxControler1)
            {
                keyboardTester.GetKeyboard();
                UpdateKeyBoard(gametime);
                keyboardTester.UpdatePreviousKeyboardState();
            }
#endif

            JumpUpdate();
        }

        private void UpdateKeyBoard(GameTime gametime)
        {
            if (keyboardTester.test(Keys.Space) &&
                    jumpState == JumpState.auSol)
            {
                Sauter();
            }
            else if (keyboardTester.test(Keys.Space) &&
                    jumpState == JumpState.arriveEnHaut)
            {
                if (keyboardTester.testEnfonceInfini(Keys.Left))
                {
                    DoubleSauter("Left");
                }
                else
                {
                    DoubleSauter("Right");
                }

            }
        }

        private void UpdateXboxControler(GameTime gametime)
        {
            if (gamePadTester.test(Buttons.A) &&
                    jumpState == JumpState.auSol)
            {
                Sauter();
            }

            else if (gamePadTester.test(Buttons.A) &&
                jumpState == JumpState.arriveEnHaut)
            {
                if (gamePadTester.GetStickX() < 0)
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
            UltimateErasme.xpManager.AddXp(XpEvents.DoubleSaut);

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
            UltimateErasme.xpManager.AddXp(XpEvents.Saut);

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
