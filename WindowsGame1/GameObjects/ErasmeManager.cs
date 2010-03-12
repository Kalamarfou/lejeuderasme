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
    public class ErasmeManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;

        public GameObject erasme;

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
        public JumpManager jumpManager;
        public AttackManager attackManager;
        public TransformationManager transformationManager;

        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
#if !XBOX
        KeyboardState previousKeyboardState = Keyboard.GetState();
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
            jumpManager = new JumpManager(game, this);
            attackManager = new AttackManager(game, this);
            transformationManager = new TransformationManager(game, this);

        }

        //gére les boutons
        public void Update(GameTime gameTime)
        {
            if (!(controllerType == ControllerType.keyboard))
            {
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One); 
               
                if (controllerType == ControllerType.xBoxControler2)
                {
                    gamePadState = GamePad.GetState(PlayerIndex.Two);
                }

                erasme.Position += new Vector2(gamePadState.ThumbSticks.Left.X * 2, 0) ;

                previousGamePadState = gamePadState;
            }
#if !XBOX
            if (controllerType == ControllerType.keyboard ||
                controllerType == ControllerType.keyboardPlusXBoxControler1)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    erasme.Position -= new Vector2(2, 0);
                }
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    erasme.Position += new Vector2(2, 0);
                }
                previousKeyboardState = keyboardState;
            }
#endif

            jumpManager.Update(gameTime, controllerType);
            attackManager.Update(gameTime, controllerType);
            buloManager.Update(gameTime, controllerType);
            transformationManager.Update(gameTime, controllerType);
            clignotageManager(gameTime);
            
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
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            attackManager.Draw(gameTime, spriteBatch);
            if (clignoteState  == ClignoteState.visible)
            {
                spriteBatch.Draw(erasme.Sprite, erasme.Position, null, Color.White, erasme.Rotation, erasme.Center, erasme.Scale, SpriteEffects.None, 0);
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
    }
}
