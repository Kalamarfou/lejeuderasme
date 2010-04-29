using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.GameObjects.enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.InputTesters;
using UltimateErasme.XP;

namespace UltimateErasme.GameObjects
{
    public class TransformationManager
    {
        ErasmeManager erasmeManager;

        public ErasmeForme erasmeForme;
        public Texture2D[] erasmeTransformation;
        public double transformationManager_OldGameTimeMilliseconds;
        int transformationIndex = 0;


        GamePadTester gamePadTester = new GamePadTester();
#if !XBOX
        KeyboardTester keyboardTester = new KeyboardTester();
#endif

        public TransformationManager(UltimateErasme game, ErasmeManager erasmeManager)
        {
            erasmeForme = ErasmeForme.erasme;
            erasmeTransformation = new Texture2D[10];
            for (int i = 0; i < 10; i++)
            {
                erasmeTransformation[i] = game.Content.Load<Texture2D>(@"Sprites\Transformation\transformation" + (i + 1));
            }
            this.erasmeManager = erasmeManager;
        }


        public void Update(GameTime gameTime, ControllerType controllerType)
        {
            if (erasmeManager.nombreDeJoueurs == NombreDeJoueurs.solo)
            {
                if (!(controllerType == ControllerType.keyboard))
                {
                    gamePadTester.ChooseGamePad(controllerType);
                    //vrai update des boutons
                    UpdateXboxControler(gameTime);
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

                TransformationManagerAnimation(gameTime);
            }
        }

        private void UpdateKeyboard(GameTime gameTime)
        {
            if (keyboardTester.test(Keys.V) &&
                erasmeForme == ErasmeForme.erasme)
            {
                SeTransformerEnVoltaire(gameTime);
            }
            else if (keyboardTester.test(Keys.V) &&
                        erasmeForme == ErasmeForme.voltaire)
            {
                SeTransformerEnErasme(gameTime);
            }
        }

        private void UpdateXboxControler(GameTime gameTime)
        {
            if (gamePadTester.test(Buttons.LeftShoulder) &&
                       erasmeForme == ErasmeForme.erasme)
            {
                SeTransformerEnVoltaire(gameTime);
            }
            else if (gamePadTester.test(Buttons.LeftShoulder) &&
                       erasmeForme == ErasmeForme.voltaire)
            {
                SeTransformerEnErasme(gameTime);
            }
        }

        private void SeTransformerEnErasme(GameTime gameTime)
        {
            UltimateErasme.xpManager.AddXp(XpEvents.Transformation);

            erasmeForme = ErasmeForme.transformationVersErasmeEnCours;
            erasmeManager.attackManager.attackState = AttackState.pasAttaque;
            erasmeManager.erasme.Sprite = erasmeTransformation[erasmeTransformation.Count<Texture2D>()-1];
            transformationIndex = erasmeTransformation.Count<Texture2D>() - 1;
            transformationManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            erasmeManager.soundManager.Transformation();
        }

        private void SeTransformerEnVoltaire(GameTime gameTime)
        {
            UltimateErasme.xpManager.AddXp(XpEvents.Transformation);

            erasmeForme = ErasmeForme.transformationVersVoltaireEnCours;
            erasmeManager.attackManager.attackState = AttackState.pasAttaque;
            erasmeManager.erasme.Sprite = erasmeTransformation[0];
            transformationIndex = 0 ;
            transformationManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            erasmeManager.soundManager.Transformation();
        }

        private void TransformationManagerAnimation(GameTime gameTime)
        {
            if (erasmeForme == ErasmeForme.transformationVersErasmeEnCours)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - transformationManager_OldGameTimeMilliseconds > 50)
                {
                    if (erasmeManager.erasme.Sprite == erasmeTransformation[0])
                    {
                        erasmeForme = ErasmeForme.erasme;
                        erasmeManager.erasme.Sprite = erasmeManager.erasmeNormal;
                    }
                    else
                    {
                        transformationIndex--;
                        erasmeManager.erasme.Sprite = erasmeTransformation[transformationIndex];
                    }
                    transformationManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
                    
                }
            }
            else if (erasmeForme == ErasmeForme.transformationVersVoltaireEnCours)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - transformationManager_OldGameTimeMilliseconds > 50)
                {
                    int poney = erasmeTransformation.Count<Texture2D>()-1;
                    if (erasmeManager.erasme.Sprite == erasmeTransformation[erasmeTransformation.Count<Texture2D>()-1])
                    {
                        erasmeForme = ErasmeForme.voltaire;
                        erasmeManager.erasme.Sprite = erasmeManager.voltaireNormal;
                    }
                    else
                    {
                        transformationIndex++;
                        erasmeManager.erasme.Sprite = erasmeTransformation[transformationIndex];
                    }
                    transformationManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
        }



    }
}
