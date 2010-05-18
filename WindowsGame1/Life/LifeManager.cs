using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace UltimateErasme.Life
{
    public class LifeManager
    {
        double lifeMax;
        double currentLife;
        double lifeMin;
        SpriteFont lifeFont;
        //Vector2 lifeFontPosition;
        Vector2 lifePosition;

        public LifeManager(UltimateErasme game)
        {
            lifeFont = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            lifePosition = new Vector2(650, 10);
            lifeMax = 100;
            lifeMin = 0;
            currentLife = 100;
            /*xpFontPosition = new Vector2(400, 520);
            xpComboPosition = new Vector2(400, 540);
            xpTotalPosition = new Vector2(10, 10);
            xpRemainingToNextLevelPosition = new Vector2(10, 30);
            xpLevelPosition = new Vector2(10, 50);*/
        }

        public void AddLife(double lifeWon)
        {
            if (currentLife + lifeWon < lifeMax)
            {
                currentLife += lifeWon;
            }
            else
            {
                currentLife = lifeMax;
            }
        }

        public void AddLife(LifeEvents lifeEvent)
        {
            switch (lifeEvent)
            {
                case LifeEvents.KillALaGraisse:
                    AddLife(0.5);
                    break;
                case LifeEvents.KillAuBulo:
                    AddLife(0.5);
                    break;
                case LifeEvents.KillALEclair:
                    AddLife(0.5);
                    break;
                case LifeEvents.KillALExplosion:
                    AddLife(0.5);
                    break;
                default:
                    return;
            }
        }

        public void SubstractLife(double lifeLost)
        {
            if (currentLife - lifeLost > lifeMin)
            {
                currentLife -= lifeLost;
            }
            else
            {
                currentLife = lifeMin;
            }
            LowLifeManager();
        }

        public void SubstractLife(LifeEvents lifeEvent)
        {
            switch (lifeEvent)
            {
                case LifeEvents.SuicideALExplosion:
                    SubstractLife(5);
                    break;
                case LifeEvents.MechantAttaque:
                    SubstractLife(5);
                    break;
                default:
                    return;
            }
        }

        private void LowLifeManager()
        {
            double ratio = (lifeMax - currentLife) / lifeMax;
            if(ratio == 1)
            {
                //Game over
            } else if(ratio > 0.9) {
                //erasme = erasme triste ?
                //Genre doom le mauvais
            }
        }

        public double GetCurrentLife()
        {
            return currentLife;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            String tempString = "" ;
            Vector2 fontOrigin = Vector2.Zero;

            // Life
            tempString = "Vie: " + currentLife.ToString() + "/" + lifeMax.ToString();
            spriteBatch.DrawString(lifeFont, tempString, lifePosition, Color.DarkBlue, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
