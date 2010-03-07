using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.ClassesDInternet.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UltimateErasme.GameObjects
{
    public class ExplosionManager
    {
        public ErasmeManager erasmeManager;

        public ParticleSystem explosion;
        public ParticleSystem smoke;
        public double explosionManager_OldGameTimeMilliseconds;
        public Vector2 explosionMochePosition;

        public Texture2D[] explosionMoche;

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
        }


        public void Update(GameTime gametime)
        {
           
        }

        public void NouvelleExplosion(Vector2 position)
        {
            BelleExplosion(position);
        }

        private void BelleExplosion(Vector2 position)
        {
            smoke.AddParticles(position);
            explosion.AddParticles(position);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
