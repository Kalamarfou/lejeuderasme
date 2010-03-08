﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.ClassesDInternet.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using UltimateErasme.GameObjects.Classes;

namespace UltimateErasme.GameObjects
{
    public class ExplosionManager
    {
        public ErasmeManager erasmeManager;

        public ParticleSystem explosion;
        public ParticleSystem smoke;
        public double explosionManager_OldGameTimeMilliseconds;

        public Texture2D[] explosionMoche;
        public Texture2D[] explosionMoyenBelle;

        public ArrayList moyenBelleExplosionCollection;
        public ArrayList mocheExplosionCollection;
        public ArrayList BelleExplosionCollisionCollection;

        public ExplosionManager(UltimateErasme game, ErasmeManager erasmeManager)
        {
            this.erasmeManager = erasmeManager;

            // create the particle systems and add them to the components list.
            // we should never see more than one explosion at once
            explosion = new ExplosionParticleSystem(game, 1, @"Sprites\ParticleSystem\explosion");
            explosion.Initialize();
            game.Components.Add(explosion);

            // but the smoke from the explosion lingers a while.
            smoke = new ExplosionSmokeParticleSystem(game, 2, @"Sprites\ParticleSystem\smoke");
            smoke.Initialize();
            game.Components.Add(smoke);

            explosionMoche = new Texture2D[6];
            for (int i = 0; i < 6; i++)
            {
                explosionMoche[i] = game.Content.Load<Texture2D>(@"Sprites\Explosion\explosion" + (i + 1));
            }
            explosionMoyenBelle = new Texture2D[16];
            for (int i = 0; i < 5; i++)
            {
                explosionMoyenBelle[i] = game.Content.Load<Texture2D>(@"Sprites\Explosion\MoyenBelle\explosion" + (i + 1));
            }
            for (int i = 0; i < 11; i++)
            {
                explosionMoyenBelle[i+5] = game.Content.Load<Texture2D>(@"Sprites\Explosion\MoyenBelle\fumée" + (i + 1));
            }

            moyenBelleExplosionCollection = new ArrayList();
            mocheExplosionCollection = new ArrayList();
            BelleExplosionCollisionCollection = new ArrayList();
        }


        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - explosionManager_OldGameTimeMilliseconds > 50)
            {
                MoyenBelleExplosionManager(gameTime);
                MocheExplosionManager(gameTime);
                BelleExplosionManager(gameTime);
                explosionManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void NouvelleExplosion(Vector2 position, GameTime gameTime)
        {
            MoyenBelleExplosion(position, gameTime);
            //MocheExplosion(position, gameTime);
            //BelleExplosion(position);
        }

        private void BelleExplosion(Vector2 position, GameTime gameTime)
        {
            smoke.AddParticles(position);
            explosion.AddParticles(position);
            //TODO
            BelleExplosionCollisionCollection.Add(new BelleExplosionCollision((int)position.X + 100, (int)position.Y + 100, 300, 300, gameTime));
        }

        private void MoyenBelleExplosion(Vector2 position, GameTime gameTime)
        {
            explosionManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            GameObject explosion = new GameObject(explosionMoyenBelle[0]);
            explosion.Position = position;
            explosion.Tag = new int();
            explosion.Tag = 0;
            moyenBelleExplosionCollection.Add(explosion);
        }
        private void MocheExplosion(Vector2 position, GameTime gameTime)
        {
            explosionManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            GameObject explosion = new GameObject(explosionMoche[0]);
            explosion.Position = position;
            explosion.Tag = new int();
            explosion.Tag = 0;
            mocheExplosionCollection.Add(explosion);
        }

        private void MoyenBelleExplosionManager(GameTime gameTime)
        {
        
            for (int i = moyenBelleExplosionCollection.Count; i > 0; i--)
            {
                GameObject explosion = (GameObject)moyenBelleExplosionCollection[i -1];
                if (explosion.Sprite == explosionMoyenBelle.Last<Texture2D>())
                {
                    moyenBelleExplosionCollection.Remove(explosion);
                }
                else
                {
                    explosion.Tag = (int)explosion.Tag + 1;
                    explosion.Sprite = explosionMoyenBelle[(int)explosion.Tag];
                }
            }
                
        }

        private void MocheExplosionManager(GameTime gameTime)
        {
            for (int i = mocheExplosionCollection.Count; i > 0; i--)
            {
                GameObject explosion = (GameObject)mocheExplosionCollection[i - 1];
                if (explosion.Sprite == explosionMoche.Last<Texture2D>())
                {
                    mocheExplosionCollection.Remove(explosion);
                }
                else
                {
                    explosion.Tag = (int)explosion.Tag + 1;
                    explosion.Sprite = explosionMoche[(int)explosion.Tag];
                }
            }
            explosionManager_OldGameTimeMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
        
        }

        private void BelleExplosionManager(GameTime gameTime)
        {
            for (int i = BelleExplosionCollisionCollection.Count ; i > 0 ; i--)
			{
                if (gameTime.TotalGameTime.TotalMilliseconds - ((BelleExplosionCollision)BelleExplosionCollisionCollection[i]).HeureDeCreation > 50 * 15)
                {
                    BelleExplosionCollisionCollection.Remove(BelleExplosionCollisionCollection[i]);
                }
			}     
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GameObject explosion in moyenBelleExplosionCollection)
            {
                spriteBatch.Draw(explosion.Sprite, explosion.Position, null, Color.White, explosion.Rotation, explosion.Center, explosion.Scale, SpriteEffects.None, 0);
            }
            foreach (GameObject explosion in mocheExplosionCollection)
            {
                spriteBatch.Draw(explosion.Sprite, explosion.Position, null, Color.White, explosion.Rotation, explosion.Center, explosion.Scale, SpriteEffects.None, 0);
            }
        }
    }
}
