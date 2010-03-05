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

namespace UltimateErasme.GameObjects
{
    class Erasme
    {
        ContentManager content;
        Rectangle viewportRect;
        Rectangle viewportRectPlus;

        public GameObject erasme;
        public GameObject bulo;

        Texture2D erasmeNormal, erasmeMonte, erasmeDescend;
        Texture2D[] erasmeAttaque;
        Texture2D[] explosionMoche;
        Texture2D graisse;
        
        int hauteurDuSol;
        Vector2 jumpVelocity;
        JumpState jumpState;

        AttackState attackState;
        double attackManager_OldGameTimeMilliseconds;
        public ArrayList boulesDeGraisse = new ArrayList();

        BuloState buloState;
        float buloPorteeMax;

        ParticleSystem explosion;
        ParticleSystem smoke;
        double explosionManager_OldGameTimeMilliseconds;
        Vector2 explosionMochePosition;
        


        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
#if !XBOX
        KeyboardState previousKeyboardState = Keyboard.GetState();
#endif


        public Erasme(UltimateErasme game, Rectangle viewportRect)
        {
            this.viewportRect = viewportRect;
            viewportRectPlus = new Rectangle(viewportRect.X, viewportRect.Y, viewportRect.Width + 100, viewportRect.Height + 100);
            this.content = game.Content;

            // create the particle systems and add them to the components list.
            // we should never see more than one explosion at once
            explosion = new ExplosionParticleSystem(game, 1, @"Sprites\ParticleSystem\explosion");
            explosion.Initialize();
            game.Components.Add(explosion);

            // but the smoke from the explosion lingers a while.
            smoke = new ExplosionSmokeParticleSystem(game, 2, @"Sprites\ParticleSystem\smoke");
            smoke.Initialize();
            game.Components.Add(smoke);

            erasmeNormal = content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme");
            erasmeMonte = content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme_blup");
            erasmeDescend = content.Load<Texture2D>(@"Sprites\Characters\Erasme\erasme_no_blup");
            graisse = content.Load<Texture2D>(@"Sprites\Graisse\graisse"); 
            erasmeAttaque = new Texture2D[8];
            explosionMoche = new Texture2D[6];
            for (int i = 0; i < 8; i++)
            {
                erasmeAttaque[i] = content.Load<Texture2D>(@"Sprites\Characters\Erasme\attaque\erasmeattaque" + (i+1));
            }
            for (int i = 0; i < 6; i++)
            {
                explosionMoche[i] = content.Load<Texture2D>(@"Sprites\Explosion\explosion" + (i + 1));
            }

            erasme = new GameObject(erasmeNormal);
            bulo = new GameObject(content.Load<Texture2D>(@"Sprites\Bulo\bulo"));

            hauteurDuSol = viewportRect.Bottom - erasme.Sprite.Height/2 - 100;
            erasme.Position = new Vector2(erasme.Sprite.Width/2, hauteurDuSol);
            jumpVelocity = new Vector2(0, 4);
            jumpState = JumpState.auSol;
            attackState = AttackState.pasAttaque;
            buloState = BuloState.pasSorti;
        }

        //gére les boutons
        public void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            erasme.Position += new Vector2(gamePadState.ThumbSticks.Left.X * 2, 0) ;

            if (gamePadState.Buttons.A == ButtonState.Pressed && 
                previousGamePadState.Buttons.A == ButtonState.Released &&
                jumpState == JumpState.auSol )
            {
                jumpState = JumpState.decollage;
                erasme.Sprite = erasmeMonte;
            }

            else if (gamePadState.Buttons.A == ButtonState.Pressed &&
                previousGamePadState.Buttons.A == ButtonState.Released 
                && jumpState == JumpState.arriveEnHaut)
            {
                jumpState = JumpState.doubleDecollage;
                if (attackState == AttackState.pasAttaque)
                {
                    erasme.Sprite = erasmeNormal;
                }
            }

            if (gamePadState.Buttons.X == ButtonState.Pressed &&
                previousGamePadState.Buttons.X == ButtonState.Released &&
                attackState == AttackState.pasAttaque)
            {
                attackState = AttackState.etape1;
                erasme.Sprite = erasmeAttaque[0];
                attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.Milliseconds;
            }

            if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed &&
                previousGamePadState.Buttons.RightShoulder == ButtonState.Released)
            {
                if (buloState == BuloState.pasSorti)
                {
                    buloState = BuloState.sorti;
                    bulo.Alive = true;
                }
                else if (buloState == BuloState.sorti)
                {
                    buloState = BuloState.pasSorti;
                    bulo.Alive = false;
                    buloPorteeMax = 0;
                }
            }
            if (buloState == BuloState.sorti)
            {
                if (gamePadState.Buttons.B == ButtonState.Pressed &&
                    previousGamePadState.Buttons.B == ButtonState.Released)
                {
                    buloState = BuloState.debutLance;
                    buloPorteeMax = erasme.Position.X + 400;
                }
            }
            else if (buloState == BuloState.debutLance || buloState == BuloState.retourLance)
            {
                if (gamePadState.Buttons.B == ButtonState.Pressed &&
                    previousGamePadState.Buttons.B == ButtonState.Released)
                {
                    
                }
            }
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
            if (keyboardState.IsKeyDown(Keys.Space) &&
                previousKeyboardState.IsKeyUp(Keys.Space) &&
                jumpState == JumpState.auSol)
            {
                jumpState = JumpState.decollage;
                if (attackState == AttackState.pasAttaque)
                {
                    erasme.Sprite = erasmeMonte;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Space) &&
                previousKeyboardState.IsKeyUp(Keys.Space) && 
                jumpState == JumpState.arriveEnHaut)
            {
                jumpState = JumpState.doubleDecollage;
                erasme.Sprite = erasmeNormal;
            }
            if (keyboardState.IsKeyDown(Keys.A) &&
                previousKeyboardState.IsKeyUp(Keys.A) && 
                attackState == AttackState.pasAttaque)
            {
                attackState = AttackState.etape1;
                erasme.Sprite = erasmeAttaque[0];
                attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.Milliseconds;
            }
            if (keyboardState.IsKeyDown(Keys.B) &&
                previousKeyboardState.IsKeyUp(Keys.B))
            {
                if (buloState == BuloState.pasSorti)
                {
                    buloState = BuloState.sorti;
                    bulo.Alive = true;
                }
                else if(buloState == BuloState.sorti)
                {
                    buloState = BuloState.pasSorti;
                    bulo.Alive = false;
                    buloPorteeMax = 0;
                }
            }
            if (buloState == BuloState.sorti)
            {
                if (keyboardState.IsKeyDown(Keys.Z) &&
                previousKeyboardState.IsKeyUp(Keys.Z))
                {
                    buloState = BuloState.debutLance;
                    buloPorteeMax = erasme.Position.X + 400;
                }
            }
            else if (buloState == BuloState.debutLance || buloState == BuloState.retourLance)
            {
                if (keyboardState.IsKeyDown(Keys.Z) &&
                previousKeyboardState.IsKeyUp(Keys.Z))
                {
                    
                }
            }

            previousKeyboardState = keyboardState;
#endif

            JumpManager();
            AttackManager(gameTime);
            GraisseManager();
            BuloManager();
            explosionManager();
        }

        private void explosionManager()
        {
            smoke.AddParticles(bulo.Position);
            explosion.AddParticles(bulo.Position);
            bulo.Alive = false;
            buloState = BuloState.pasSorti;
        }

        //gére le bulo
        private void BuloManager()
        {
            if (buloState != BuloState.pasSorti)
            {
                if (buloState == BuloState.sorti)
                {
                    if (erasme.Sprite == erasmeMonte)
                    {
                        bulo.Position = erasme.Position + new Vector2(30, 100);
                    }
                    else if (erasme.Sprite == erasmeDescend)
                    {
                        bulo.Position = erasme.Position + new Vector2(20, -50);
                    }
                    else
                    {
                        bulo.Position = erasme.Position + (new Vector2(100, 35));
                    }
                    bulo.Rotation = erasme.Rotation;
                }

                else if (buloState == BuloState.debutLance)
                {
                    if (bulo.Position.X < buloPorteeMax)
                    {
                        bulo.Position += new Vector2(5, 0);
                        bulo.Rotation += 0.5f;
                    }
                    else
                    {
                        buloState = BuloState.retourLance;
                    }
                }
                else if (buloState == BuloState.retourLance)
                {
                    
                    if (bulo.Position.Y > erasme.Position.Y)
                    {
                        bulo.Position -= new Vector2(0, 1);
                        bulo.Rotation += 0.5f;
                    }
                    else if (bulo.Position.Y < erasme.Position.Y)
                    {
                        bulo.Position += new Vector2(0, 1);
                        bulo.Rotation += 0.5f;
                    }
                    if (bulo.Position.X > erasme.Position.X)
                    {
                        bulo.Position -= new Vector2(5, 0);
                        bulo.Rotation += 0.5f;
                    }
                    else
                    {
                        buloState = BuloState.sorti;
                    }
                }
            }
        }

        //gére les attaques
        private void AttackManager(GameTime gameTime)
        {
            if (attackState != AttackState.pasAttaque)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - attackManager_OldGameTimeMilliseconds > 100)
                {
                    attackManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
                    if (attackState == AttackState.etape8)
                    {
                        erasme.Sprite = erasmeNormal;
                        attackState = AttackState.pasAttaque;
                    }
                    else
                    {
                        erasme.Sprite = erasmeAttaque[(int)attackState];
                        attackState++;
                    }
                    if (attackState == AttackState.etape4)
                    {
                        TirerBouleDeGraisse();
                    }
                }
            }
        }

        //gére les diferentes etapes du saut et du double saut.
        private void JumpManager()
        {
            if ( jumpState == JumpState.decollage)
            {
                erasme.Position -= jumpVelocity;
                if (erasme.Position.Y <= hauteurDuSol - 80)
                {
                    jumpState = JumpState.arriveEnHaut;
                }
            }
            if (jumpState == JumpState.arriveEnHaut)
            {
                erasme.Position -= jumpVelocity/2;
                if (erasme.Position.Y <= hauteurDuSol - 100)
                {
                    jumpState = JumpState.toutEnHaut;
                }
            }
            if (jumpState == JumpState.doubleDecollage)
            {
                erasme.Rotation += 0.2f;
                erasme.Position -= jumpVelocity;
                if (erasme.Position.Y <= hauteurDuSol - 180)
                {
                    jumpState = JumpState.doubleArriveEnHaut;
                }
            }
            if (jumpState == JumpState.doubleArriveEnHaut)
            {
                erasme.Rotation += 0.2f;
                erasme.Position -= jumpVelocity / 2;
                if (erasme.Position.Y <= hauteurDuSol - 200)
                {
                    jumpState = JumpState.doubleToutEnHaut;
                }
            }
            if (jumpState == JumpState.doubleToutEnHaut)
            {
                erasme.Rotation += 0.2f;
                jumpState = JumpState.doubleRepartEnBas;
            }
            if (jumpState == JumpState.doubleRepartEnBas)
            {
                erasme.Rotation += 0.2f;
                erasme.Position += jumpVelocity / 2;
                if (erasme.Position.Y >= hauteurDuSol - 180)
                {
                    jumpState = JumpState.doubleAtterissage;
                }
            }
            if (jumpState == JumpState.doubleAtterissage)
            {
                erasme.Rotation += 0.2f;
                erasme.Position += jumpVelocity;
                if (erasme.Position.Y >= hauteurDuSol - 80)
                {
                    jumpState = JumpState.atterissage;
                    erasme.Rotation = 0;
                }
            }
            if (jumpState == JumpState.toutEnHaut)
            {
                jumpState = JumpState.repartEnBas;
                if (attackState == AttackState.pasAttaque)
                {
                    erasme.Sprite = erasmeDescend;
                }
                
            }
            if (jumpState == JumpState.repartEnBas)
            {
                erasme.Position += jumpVelocity / 2;
                if (erasme.Position.Y >= hauteurDuSol - 80)
                {
                    jumpState = JumpState.atterissage;
                }
            }
            if (jumpState == JumpState.atterissage)
            {
                erasme.Position += jumpVelocity;
                if (erasme.Position.Y >= hauteurDuSol)
                {
                    jumpState = JumpState.auSol;
                    if (attackState == AttackState.pasAttaque)
                    {
                        erasme.Sprite = erasmeNormal;
                    }
                }
            }

        }

        private void GraisseManager()
        {
            if (boulesDeGraisse.Count > 0)
            {
                foreach (GameObject boule in boulesDeGraisse)
                {
                    boule.Position += boule.Velocity;
                }
                for (int i = boulesDeGraisse.Count-1; i >= 0; i--)
                {
                    if (!viewportRectPlus.Contains(new Point(
                        (int)((GameObject)boulesDeGraisse[i]).Position.X,
                        (int)((GameObject)boulesDeGraisse[i]).Position.Y)))
                    {
                        boulesDeGraisse.RemoveAt(i);
                    }
                }
            }
            
        }

        private void TirerBouleDeGraisse()
        {
            if (erasme.Rotation == 0)
            {
                GameObject boule = new GameObject(graisse);

                boule.Velocity = new Vector2((float)Math.Cos(erasme.Rotation) * 5f,
                            (float)Math.Sin(erasme.Rotation) * 5f);

                boule.Position = erasme.Position + new Vector2(30, 30);
                boule.Rotation = erasme.Rotation;
                boulesDeGraisse.Add(boule);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                GameObject boule = new GameObject(graisse);
                boule.Rotation = i * MathHelper.PiOver4;
                boule.Velocity = new Vector2((float)Math.Cos(boule.Rotation) * 5f,
                            (float)Math.Sin(boule.Rotation) * 5f);

                boule.Position = erasme.Position + new Vector2(30, 30);
                boulesDeGraisse.Add(boule);
                }
            }
        }

        //dessine erasme
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GameObject boule in boulesDeGraisse)
            {
                spriteBatch.Draw(boule.Sprite, boule.Position, null, Color.White, boule.Rotation, boule.Center, boule.Scale, SpriteEffects.None, 0);
            
            }
            spriteBatch.Draw(erasme.Sprite, erasme.Position, null, Color.White, erasme.Rotation, erasme.Center, erasme.Scale, SpriteEffects.None, 0);
            if (bulo.Alive)
            {
                spriteBatch.Draw(bulo.Sprite, bulo.Position, null, Color.White, bulo.Rotation, bulo.Center, bulo.Scale, SpriteEffects.None, 0);
            }
        }

    }
}
