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
    public class PlayersManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public ErasmeManager premierJoueur, deuxiemeJoueur;
        public NombreDeJoueurs nombreDeJoueurs = NombreDeJoueurs.solo;
        bool deuxJoueursAuthorise = true;

        public PlayersManager(UltimateErasme game, Rectangle viewportRect)
        {
            this.game = game;
            this.viewportRect = viewportRect;
            premierJoueur = new ErasmeManager(game, viewportRect);
            deuxiemeJoueur = null;
        }

        public void Update(GameTime gameTime)
        {
            premierJoueur.Update(gameTime);
            if (deuxJoueursAuthorise)
            {
                if (nombreDeJoueurs == NombreDeJoueurs.deuxJoueurs)
                {
                    if (deuxiemeJoueur != null)
                    {
                        deuxiemeJoueur.Update(gameTime);
                    }
                    ManagePressEscape(gameTime);
                }
                else if (nombreDeJoueurs == NombreDeJoueurs.solo)
                {
                    if (premierJoueur.transformationManager.erasmeForme == ErasmeForme.erasme ||
                        premierJoueur.transformationManager.erasmeForme == ErasmeForme.voltaire)
                    {
                        ManagePressStart(gameTime);
                    }
                }
            }
        }

        private void ManagePressEscape(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                EnleverJoueur2();
            }

            GamePadState gamePadState2 = GamePad.GetState(PlayerIndex.Two);
            if (gamePadState2.Buttons.Back == ButtonState.Pressed)
            {
                EnleverJoueur2();
            }
#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Delete))
            {
                EnleverJoueur2();
            }
#endif
        }

        private void ManagePressStart(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.Buttons.Start == ButtonState.Pressed)
            {
                AjouterJoueur2();
                premierJoueur.controllerType = ControllerType.keyboard;
                deuxiemeJoueur.controllerType = ControllerType.xBoxControler1;
            }

            GamePadState gamePadState2 = GamePad.GetState(PlayerIndex.Two);
            if (gamePadState2.Buttons.Start == ButtonState.Pressed)
            {
                AjouterJoueur2();
                deuxiemeJoueur.controllerType = ControllerType.xBoxControler2;
            }
#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                AjouterJoueur2();
                premierJoueur.controllerType = ControllerType.xBoxControler1;
                deuxiemeJoueur.controllerType = ControllerType.keyboard;
            }
#endif
        }

        private void AjouterJoueur2()
        {
            premierJoueur.nombreDeJoueurs = NombreDeJoueurs.deuxJoueurs;

            deuxiemeJoueur = new ErasmeManager(game, viewportRect);
            deuxiemeJoueur.erasme.Position = premierJoueur.erasme.Position;
            deuxiemeJoueur.erasme.Rotation = premierJoueur.erasme.Rotation;
            deuxiemeJoueur.jumpManager.jumpState = premierJoueur.jumpManager.jumpState;
            deuxiemeJoueur.nombreDeJoueurs = NombreDeJoueurs.deuxJoueurs;
            deuxiemeJoueur.numeroDuJoueur = NumeroDuJoueur.deux;
            if (premierJoueur.transformationManager.erasmeForme == ErasmeForme.voltaire)
            {
                deuxiemeJoueur.erasme.Sprite = deuxiemeJoueur.erasmeNormal;
                deuxiemeJoueur.transformationManager.erasmeForme = ErasmeForme.erasme;
            }
            else if (premierJoueur.transformationManager.erasmeForme == ErasmeForme.erasme)
            {
                deuxiemeJoueur.erasme.Sprite = deuxiemeJoueur.voltaireNormal;
                deuxiemeJoueur.transformationManager.erasmeForme = ErasmeForme.voltaire;
            }

            this.nombreDeJoueurs = NombreDeJoueurs.deuxJoueurs;
        }

        private void EnleverJoueur2()
        {
            premierJoueur.nombreDeJoueurs = NombreDeJoueurs.solo;
            this.nombreDeJoueurs = NombreDeJoueurs.solo;
            premierJoueur.controllerType = ControllerType.keyboardPlusXBoxControler1;

            deuxiemeJoueur = null;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            premierJoueur.Draw(gameTime, spriteBatch);
            if (nombreDeJoueurs == NombreDeJoueurs.deuxJoueurs)
            {
                if (deuxiemeJoueur != null)
                {
                    deuxiemeJoueur.Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
