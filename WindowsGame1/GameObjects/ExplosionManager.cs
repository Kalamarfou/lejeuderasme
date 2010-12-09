using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.ClassesDInternet.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using UltimateErasme.GameObjects.Classes;
using UltimateErasme.GameObjects.enums;

namespace UltimateErasme.GameObjects
{
    public class ExplosionManager
    {
        public UltimateErasme game;

        public ParticleSystem explosion;
        public ParticleSystem smoke;
        public double explosionManager_OldGameTimeMilliseconds;

        public Texture2D[] explosionMoche;
        public Texture2D[] explosionMoyenBelle;

        public ArrayList moyenBelleExplosionCollection;
        public ArrayList mocheExplosionCollection;
        public ArrayList BelleExplosionCollisionCollection;

        public ExplosionManager(UltimateErasme game)
        {
            this.game = game;

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

        public void NouvelleExplosion(Vector2 position, GameTime gameTime, ExplosionType explosionType)
        {
            switch (explosionType)
            {
                case ExplosionType.moche:
                    BelleExplosion(position, gameTime);
                    break;
                case ExplosionType.moyenBelle:
                    BelleExplosion(position, gameTime);
                    break;
                case ExplosionType.belle:
                    BelleExplosion(position, gameTime);
                    break;
                default:
                    break;
            }
            
        }

        private void BelleExplosion(Vector2 position, GameTime gameTime)
        {
            smoke.AddParticles(position);
            explosion.AddParticles(position);
            //TODO
            BelleExplosionCollisionCollection.Add(new BelleExplosionCollision((int)position.X + 50, (int)position.Y + 50, 200, 200, gameTime));
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
            for (int i = BelleExplosionCollisionCollection.Count; i > 0 ; i--)
			{
                if (gameTime.TotalGameTime.TotalMilliseconds - ((BelleExplosionCollision)BelleExplosionCollisionCollection[i-1]).HeureDeCreation > 50 * 12)
                {
                    BelleExplosionCollisionCollection.Remove(BelleExplosionCollisionCollection[i-1]);
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

        internal void AjouterExplosionsAttaquesBox(ArrayList explosionsAttaquesBox)
        {
            foreach (GameObject explosion in mocheExplosionCollection)
            {
                Rectangle rect = new Rectangle((int)explosion.Position.X, (int)explosion.Position.Y, explosion.Sprite.Width, explosion.Sprite.Height);
                explosionsAttaquesBox.Add(rect);
            }
            foreach (GameObject explosion in moyenBelleExplosionCollection)
            {
                Rectangle rect = new Rectangle((int)explosion.Position.X, (int)explosion.Position.Y, explosion.Sprite.Width, explosion.Sprite.Height);
                explosionsAttaquesBox.Add(rect);
            }
            foreach (BelleExplosionCollision explosion in BelleExplosionCollisionCollection)
            {
                explosionsAttaquesBox.Add(explosion.Rectangle);
            }
        }
    }
}
