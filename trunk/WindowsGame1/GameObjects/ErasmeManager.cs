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
using UltimateErasme.InputTesters;

namespace UltimateErasme.GameObjects
{
    public class ErasmeManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;

        public GameObject erasme;
        //pour mettre les accessoires d'érasme
        public ErasmeAccessoiresCollection ErasmeAccessoires { get; set; }

        public ControllerType controllerType = ControllerType.keyboardPlusXBoxControler1;
        public NombreDeJoueurs nombreDeJoueurs = NombreDeJoueurs.solo;
        public NumeroDuJoueur numeroDuJoueur = NumeroDuJoueur.un;

        public bool clignote = false;
        public ClignoteState clignoteState = ClignoteState.visible;
        public double HeureDebutClignotage;

        public Texture2D erasmeNormal;
        public Texture2D voltaireNormal;

        public SoundManager soundManager;
        public BuloManager buloManager;
        public ErasmeJumpManager jumpManager;
        public AttackManager attackManager;
        public TransformationManager transformationManager;

        GamePadTester gamePadTester = new GamePadTester();
#if !XBOX
        KeyboardTester keyboardTester = new KeyboardTester();
#endif


        public ErasmeManager(UltimateErasme game, Rectangle viewportRect)
        {
            this.viewportRect = viewportRect;
            viewportRectPlus = new Rectangle(viewportRect.X, viewportRect.Y, viewportRect.Width + 100, viewportRect.Height + 100);
            this.game = game;

            erasmeNormal = game.Content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme");
            voltaireNormal = game.Content.Load<Texture2D>(@"Sprites\Characters\Voltaire\voltaire");
            erasme = new GameObject(erasmeNormal);

            soundManager = new SoundManager();
            buloManager = new BuloManager(game, this);
            jumpManager = new ErasmeJumpManager(game, this);
            attackManager = new AttackManager(game, this);
            transformationManager = new TransformationManager(game, this);
            ErasmeAccessoires = new ErasmeAccessoiresCollection();

            ErasmeAccessoires.AddDirectFromTexture(game.Content.Load<Texture2D>(@"Sprites\Characters\Accessoires\criniere"));
        }

        public ErasmeManager()
        {

        }

        //gére les boutons
        public virtual void Update(GameTime gameTime)
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

            jumpManager.Update(gameTime, controllerType);
            attackManager.Update(gameTime, controllerType);
            buloManager.Update(gameTime, controllerType);
            transformationManager.Update(gameTime, controllerType);
            clignotageManager(gameTime);
            ErasmeAccessoires.Update(gameTime, erasme.Position, erasme.Rotation);
            
        }

        private void UpdateKeyboard(GameTime gameTime)
        {
            if (keyboardTester.testEnfonceInfini(Keys.Left))
            {
                erasme.Position -= new Vector2(2, 0);
            }
            if (keyboardTester.testEnfonceInfini(Keys.Right))
            {
                erasme.Position += new Vector2(2, 0);
            }
        }

        private void UpdateXboxControler(GameTime gameTime)
        {
            erasme.Position += new Vector2(gamePadTester.GetStickX() * 2, 0);
        }

        private void clignotageManager(GameTime gameTime)
        {
            if (clignote)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - HeureDebutClignotage > 1000)
                {
                    clignote = false;
                    clignoteState = ClignoteState.visible;
                }
                else if (clignoteState == ClignoteState.visible)
                {
                    clignoteState = ClignoteState.invisible;
                }
                else if (clignoteState == ClignoteState.invisible)
                {
                    clignoteState = ClignoteState.visible;
                }
            }
        }

        //dessine erasme
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            attackManager.Draw(gameTime, spriteBatch);
            if (clignoteState  == ClignoteState.visible)
            {
                spriteBatch.Draw(erasme.Sprite, erasme.Position, null, Color.White, erasme.Rotation, erasme.Center, erasme.Scale, SpriteEffects.None, 0);
                if (transformationManager.erasmeForme == ErasmeForme.erasme || transformationManager.erasmeForme == ErasmeForme.voltaire )
                {
                    ErasmeAccessoires.Draw(gameTime, spriteBatch);
                }
            }
            buloManager.Draw(gameTime, spriteBatch);            
        }



        public Rectangle getVulnerableBox()
        {
            if (attackManager.attackState != AttackState.pasAttaque && transformationManager.erasmeForme == ErasmeForme.voltaire)
            {
                return new Rectangle((int)erasme.Position.X, (int)erasme.Position.Y, erasme.Sprite.Width - 60, erasme.Sprite.Height);
            }
            else if (transformationManager.erasmeForme == ErasmeForme.voltaire)
            {
                return new Rectangle((int)erasme.Position.X, (int)erasme.Position.Y, erasme.Sprite.Width, erasme.Sprite.Height);
            }
            else if (transformationManager.erasmeForme == ErasmeForme.erasme)
            {
                return new Rectangle((int)erasme.Position.X, (int)erasme.Position.Y, erasme.Sprite.Width, erasme.Sprite.Height);
            }
            else
            {
                return new Rectangle(0,0,0,0);
            }
            
        }

        internal void RemettreErasmeAuDebut()
        {
            erasme.Position = new Vector2(0,jumpManager.hauteurDuSol);
        }

        internal void RemettreErasmeALaFin()
        {
            erasme.Position = new Vector2(600, jumpManager.hauteurDuSol);
        }
    }
}
