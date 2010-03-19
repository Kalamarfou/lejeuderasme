using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UltimateErasme.GameObjects
{
    public class MechantJumpManager : JumpManager
    {
        Mechant mechant;
        public MechantJumpManager(UltimateErasme game, Mechant mechant)
        {
            hauteurDuSol = game.playerManager.premierJoueur.viewportRect.Bottom - game.playerManager.premierJoueur.erasme.Sprite.Height / 2 - 100;
            this.mechant = mechant;
        }

        public void Update(GameTime gametime)
        {
            JumpUpdate();
        }

        public void DoubleSauter(SpriteEffects sens)
        {
            jumpState = JumpState.doubleDecollage;
            if (sens == SpriteEffects.FlipHorizontally)
            {
                sensDuDoubleSaut = "Right";
            }
            else
            {
                sensDuDoubleSaut = "Left";
            }
            
        }

        public void Sauter()
        {
            jumpState = JumpState.decollage;
        }

        private void JumpUpdate()
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
            mechant.MechantGameObject.Position += jumpVelocity;
            if (mechant.MechantGameObject.Position.Y >= hauteurDuSol)
            {
                JumpTerminerSaut();
            }
        }

        private void JumpTerminerSaut()
        {
            jumpState = JumpState.auSol;
            
        }

        private void JumpRepartEnBas()
        {
            mechant.MechantGameObject.Position += jumpVelocity / 2;
            if (mechant.MechantGameObject.Position.Y >= hauteurDuSol - 80)
            {
                jumpState = JumpState.atterissage;
            }
        }

        private void JumpToutEnHaut()
        {
            jumpState = JumpState.repartEnBas;
        }

        private void JumpDoubleAtterissage()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            mechant.MechantGameObject.Position += jumpVelocity;
            if (mechant.MechantGameObject.Position.Y >= hauteurDuSol - 80)
            {
                jumpState = JumpState.atterissage;
                mechant.MechantGameObject.Rotation = 0;
            }
        }

        private void JumpDoubleRepartEnBas()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            mechant.MechantGameObject.Position += jumpVelocity / 2;
            if (mechant.MechantGameObject.Position.Y >= hauteurDuSol - 180)
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
            mechant.MechantGameObject.Position -= jumpVelocity / 2;
            if (mechant.MechantGameObject.Position.Y <= hauteurDuSol - 200)
            {
                jumpState = JumpState.doubleToutEnHaut;
            }
        }

        private void JumpDoubleDecollage()
        {
            RotationDoubleSautManager(sensDuDoubleSaut);
            mechant.MechantGameObject.Position -= jumpVelocity;
            if (mechant.MechantGameObject.Position.Y <= hauteurDuSol - 180)
            {
                jumpState = JumpState.doubleArriveEnHaut;
            }
        }

        private void JumpArriveEnHaut()
        {
            mechant.MechantGameObject.Position -= jumpVelocity / 2;
            if (mechant.MechantGameObject.Position.Y <= hauteurDuSol - 100)
            {
                jumpState = JumpState.toutEnHaut;
            }
        }

        private void JumpDecollage()
        {
            mechant.MechantGameObject.Position -= jumpVelocity;
            if (mechant.MechantGameObject.Position.Y <= hauteurDuSol - 80)
            {
                jumpState = JumpState.arriveEnHaut;
            }
        }

        private void RotationDoubleSautManager(string sensDuDoubleSaut)
        {
            if (sensDuDoubleSaut == "Left")
            {
                mechant.MechantGameObject.Rotation -= 0.2f;
            }
            else
            {
                mechant.MechantGameObject.Rotation += 0.2f;
            }
        }
    }
}
