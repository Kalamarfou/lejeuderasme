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

namespace UltimateErasme.GameObjects
{
    public class ErasmeManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;

        public GameObject erasme;


        public Texture2D erasmeNormal;

        public SoundManager soundManager;
        public BuloManager buloManager;
        public JumpManager jumpManager;
        public AttackManager attackManager;

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
            erasme = new GameObject(erasmeNormal);

            soundManager = new SoundManager();
            buloManager = new BuloManager(game, this);
            jumpManager = new JumpManager(game, this);
            attackManager = new AttackManager(game, this);

        }

        //gére les boutons
        public void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            erasme.Position += new Vector2(gamePadState.ThumbSticks.Left.X * 2, 0) ;

            previousGamePadState = gamePadState;
#if !XBOX
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
#endif

            jumpManager.Update(gameTime);
            attackManager.Update(gameTime);
            buloManager.Update(gameTime); ;
            
        }

        //dessine erasme
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            attackManager.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(erasme.Sprite, erasme.Position, null, Color.White, erasme.Rotation, erasme.Center, erasme.Scale, SpriteEffects.None, 0);
            buloManager.Draw(gameTime, spriteBatch);
            
        }

    }
}
