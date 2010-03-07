using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme.GameObjects
{
    public class AttackManager
    {
        public ErasmeManager erasmeManager;

        public Texture2D[] erasmeAttaque;
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
            attackState = AttackState.pasAttaque;
        }



        public void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.Buttons.X == ButtonState.Pressed &&
               previousGamePadState.Buttons.X == ButtonState.Released &&
               attackState == AttackState.pasAttaque)
            {
                attackState = AttackState.etape1;
                erasmeManager.erasme.Sprite = erasmeAttaque[0];
                attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.Milliseconds;
            }
            previousGamePadState = gamePadState;
#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A) &&
                previousKeyboardState.IsKeyUp(Keys.A) && 
                attackState == AttackState.pasAttaque)
            {
                attackState = AttackState.etape1;
                erasmeManager.erasme.Sprite = erasmeAttaque[0];
                attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.Milliseconds;
            }
            previousKeyboardState = keyboardState;
#endif

            AttackManagerAnimation(gameTime);
            graisseManager.Update(gameTime);
        }

        //gére les attaques
        private void AttackManagerAnimation(GameTime gameTime)
        {
            if (attackState != AttackState.pasAttaque)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - attackManager_OldGameTimeMilliseconds > 100)
                {
                    attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
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
            }
        }




        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graisseManager.Draw(gameTime,spriteBatch);
        }
    }
}
