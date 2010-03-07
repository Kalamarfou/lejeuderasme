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

        public void Update(GameTime gametime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
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
                DoubleSauter();
            }
            previousGamePadState = gamePadState;

#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
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
                DoubleSauter();
            }
            previousKeyboardState = keyboardState;
#endif

            JumpUpdate();
        }

        private void DoubleSauter()
        {
            jumpState = JumpState.doubleDecollage;
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
            if (jumpState == JumpState.decollage)
            {
                erasmeManager.erasme.Position -= jumpVelocity;
                if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 80)
                {
                    jumpState = JumpState.arriveEnHaut;
                }
            }
            if (jumpState == JumpState.arriveEnHaut)
            {
                erasmeManager.erasme.Position -= jumpVelocity / 2;
                if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 100)
                {
                    jumpState = JumpState.toutEnHaut;
                }
            }
            if (jumpState == JumpState.doubleDecollage)
            {
                erasmeManager.erasme.Rotation += 0.2f;
                erasmeManager.erasme.Position -= jumpVelocity;
                if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 180)
                {
                    jumpState = JumpState.doubleArriveEnHaut;
                }
            }
            if (jumpState == JumpState.doubleArriveEnHaut)
            {
                erasmeManager.erasme.Rotation += 0.2f;
                erasmeManager.erasme.Position -= jumpVelocity / 2;
                if (erasmeManager.erasme.Position.Y <= hauteurDuSol - 200)
                {
                    jumpState = JumpState.doubleToutEnHaut;
                }
            }
            if (jumpState == JumpState.doubleToutEnHaut)
            {
                erasmeManager.erasme.Rotation += 0.2f;
                jumpState = JumpState.doubleRepartEnBas;
            }
            if (jumpState == JumpState.doubleRepartEnBas)
            {
                erasmeManager.erasme.Rotation += 0.2f;
                erasmeManager.erasme.Position += jumpVelocity / 2;
                if (erasmeManager.erasme.Position.Y >= hauteurDuSol - 180)
                {
                    jumpState = JumpState.doubleAtterissage;
                }
            }
            if (jumpState == JumpState.doubleAtterissage)
            {
                erasmeManager.erasme.Rotation += 0.2f;
                erasmeManager.erasme.Position += jumpVelocity;
                if (erasmeManager.erasme.Position.Y >= hauteurDuSol - 80)
                {
                    jumpState = JumpState.atterissage;
                    erasmeManager.erasme.Rotation = 0;
                }
            }
            if (jumpState == JumpState.toutEnHaut)
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
            if (jumpState == JumpState.repartEnBas)
            {
                erasmeManager.erasme.Position += jumpVelocity / 2;
                if (erasmeManager.erasme.Position.Y >= hauteurDuSol - 80)
                {
                    jumpState = JumpState.atterissage;
                }
            }
            if (jumpState == JumpState.atterissage)
            {
                erasmeManager.erasme.Position += jumpVelocity;
                if (erasmeManager.erasme.Position.Y >= hauteurDuSol)
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
            }
        }

    }
}
