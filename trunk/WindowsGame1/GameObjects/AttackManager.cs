﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.GameObjects.enums;

namespace UltimateErasme.GameObjects
{
    public class AttackManager
    {
        public ErasmeManager erasmeManager;
        public Texture2D[] erasmeAttaque;
        public Texture2D[] voltaireAttaque;
        public AttackState attackState;
        public double attackManager_OldGameTimeMilliseconds;

        public GraisseManager graisseManager;


        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
#if !XBOX
        KeyboardState previousKeyboardState = Keyboard.GetState();
#endif



        public AttackManager(UltimateErasme game, ErasmeManager erasmeManager)
        {
            this.erasmeManager = erasmeManager;
            graisseManager = new GraisseManager(game, erasmeManager);
            erasmeAttaque = new Texture2D[8];
            for (int i = 0; i < 8; i++)
            {
                erasmeAttaque[i] = game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\attaque\erasmeattaque" + (i + 1));
            }
            voltaireAttaque = new Texture2D[6];
            for (int i = 0; i < 6; i++)
            {
                voltaireAttaque[i] = game.Content.Load<Texture2D>(@"Sprites\Characters\Voltaire\attaque\attaquevoltaire" + (i + 1));
            }
            attackState = AttackState.pasAttaque;
        }


        public void Update(GameTime gameTime, ControllerType controllerType)
        {
            if (!(controllerType == ControllerType.keyboard))
            {
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                if (controllerType == ControllerType.xBoxControler2)
                {
                    gamePadState = GamePad.GetState(PlayerIndex.Two);
                }
                if (gamePadState.Buttons.X == ButtonState.Pressed &&
                   previousGamePadState.Buttons.X == ButtonState.Released &&
                   attackState == AttackState.pasAttaque)
                {
                    Attaquer(gameTime);
                }
                previousGamePadState = gamePadState;
            }
#if !XBOX
            if (controllerType == ControllerType.keyboard ||
                controllerType == ControllerType.keyboardPlusXBoxControler1)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.A) &&
                    previousKeyboardState.IsKeyUp(Keys.A) &&
                    attackState == AttackState.pasAttaque)
                {
                    Attaquer(gameTime);
                }
                previousKeyboardState = keyboardState;
            }
#endif

            AttackManagerAnimation(gameTime);
            graisseManager.Update(gameTime);
        }

        private void Attaquer(GameTime gameTime)
        {
            attackState = AttackState.etape1;
            if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.erasme)
            {
                erasmeManager.erasme.Sprite = erasmeAttaque[0];
            }
            else if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.voltaire)
            {
                erasmeManager.erasme.Sprite = voltaireAttaque[0];
                erasmeManager.soundManager.AttaqueVoltaire();
            }
            attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
        }

        //gére les attaques
        private void AttackManagerAnimation(GameTime gameTime)
        {
            if (attackState != AttackState.pasAttaque)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - attackManager_OldGameTimeMilliseconds > 100)
                {
                    attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
                    if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.erasme)
                    {
                        if (attackState == AttackState.etape8)
                        {
                            erasmeManager.erasme.Sprite = erasmeManager.erasmeNormal;
                            attackState = AttackState.pasAttaque;
                        }
                        else
                        {
                            erasmeManager.erasme.Sprite = erasmeAttaque[(int)attackState];
                            attackState++;
                        }
                        if (attackState == AttackState.etape4)
                        {
                            graisseManager.TirerBouleDeGraisse();
                        }
                    }
                    if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.voltaire)
                    {
                        if (attackState == AttackState.etape6)
                        {
                            erasmeManager.erasme.Sprite = erasmeManager.voltaireNormal;
                            attackState = AttackState.pasAttaque;
                        }
                        else
                        {
                            erasmeManager.erasme.Sprite = voltaireAttaque[(int)attackState];
                            attackState++;
                        }
                    }
                }
            }
        }




        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graisseManager.Draw(gameTime,spriteBatch);
        }

        internal void AjouterPersonnagesAttaquesBox(ArrayList graisseAttaquesBox, ArrayList voltaireAttaquesBox, ArrayList transformationAttaquesBox)
        {
            graisseManager.AjouterPersonnagesAttaquesBox(graisseAttaquesBox);
            if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.voltaire)
            {
                if (attackState == AttackState.etape4 || attackState == AttackState.etape5 || attackState == AttackState.etape6)
                {
                    Rectangle rect = new Rectangle((int)erasmeManager.erasme.Position.X + 175, (int)erasmeManager.erasme.Position.Y, 60, erasmeManager.erasme.Sprite.Height);
                    voltaireAttaquesBox.Add(rect);
                }
            }
            if (erasmeManager.transformationManager.erasmeForme == ErasmeForme.transformationVersErasmeEnCours ||
                erasmeManager.transformationManager.erasmeForme == ErasmeForme.transformationVersVoltaireEnCours)
            {
                Rectangle rect = new Rectangle((int)erasmeManager.erasme.Position.X, (int)erasmeManager.erasme.Position.Y, erasmeManager.erasme.Sprite.Width, erasmeManager.erasme.Sprite.Height);
                transformationAttaquesBox.Add(rect);
            }
        }
    }
}
