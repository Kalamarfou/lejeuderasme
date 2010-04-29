using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace UltimateErasme.XP
{
    public class XpManager
    {
        int level = 1;
        int xpRemainingToNextLevel = 10;
        int totalXP = 0;
        int lastXpObtained = 0;
        int XpCombo = 0;
        string XpRaison = "";
        
        Timer timerXp;
        Timer timerCombo;
        bool afficherCombo = false;

        SpriteFont xpFont;
        Vector2 xpFontPosition;
        Vector2 xpComboPosition;
        Vector2 xpTotalPosition;
        Vector2 xpLevelPosition;
        Vector2 xpRemainingToNextLevelPosition;

        public XpManager(UltimateErasme game)
        {
            xpFont = game.Content.Load<SpriteFont>(@"Fonts\XpFont");
            xpFontPosition = new Vector2(400, 520);
            xpComboPosition = new Vector2(400, 540);
            xpTotalPosition = new Vector2(10, 10);
            xpRemainingToNextLevelPosition = new Vector2(10, 30);
            xpLevelPosition = new Vector2(10, 50);

            timerXp = new Timer();
            timerCombo = new Timer();
        }



        public void Update(GameTime gameTime)
        {
        }

        public void AddXp(XpEvents xpEvent)
        {
            switch (xpEvent)
            {
                case XpEvents.Saut:
                    AddXp(1, "Saut");
                    break;
                case XpEvents.DoubleSaut:
                    AddXp(2, "Double saut");
                    break;
                case XpEvents.KillALaGraisse:
                    AddXp(3, "Kill a la graisse");
                    break;
                case XpEvents.KillAuBulo:
                    AddXp(3, "Kill a coup de Bulo");
                    break;
                case XpEvents.KillALEclair:
                    AddXp(5, "Kill a coup d'eclair de Voltaire");
                    break;
                case XpEvents.Transformation:
                    AddXp(1, "Transformation reussie");
                    break;
                case XpEvents.KillALExplosion:
                    AddXp(4, "Kill a l'explosion");
                    break;
                case XpEvents.SuicideALExplosion:
                    AddXp(-4, "Suicide a l'explosion");
                    break;
                case XpEvents.SortageDeBulo:
                    AddXp(1, "Sortage de Bulo");
                    break;
                case XpEvents.RentrageDeBulo:
                    AddXp(1, "Rentrage de Bulo");
                    break;
                case XpEvents.AttaqueTournoyante:
                    AddXp(1, "Attaque Tournoyante");
                    break;
                default:
                    return;
            }
            timerXp.Dispose();
            timerXp = new Timer(1500) { Enabled = true };
            timerXp.Elapsed += new ElapsedEventHandler(timerXp_Elapsed);
        }

        void timerXp_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (((Timer)sender) == timerXp)
            {
                timerXp.Enabled = false;
            }
        }

        private void AddXp(int xpGagne, string raison)
        {
            totalXP += xpGagne;
            lastXpObtained = xpGagne;
            xpRemainingToNextLevel -= xpGagne;
            ComboManager(xpGagne);
            LevelUpManager();

            XpRaison = raison;
        }

        private void ComboManager(int xpGagne)
        {
            XpCombo += xpGagne;
            if (timerCombo.Enabled)
            {
                afficherCombo = true;
            }
            timerCombo.Dispose();
            timerCombo = new Timer(2000) { Enabled = true };
            timerCombo.Elapsed += new ElapsedEventHandler(timerCombo_Elapsed);
        }

        void timerCombo_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (((Timer)sender) == timerCombo)
            {
                timerCombo.Enabled = false;
                XpCombo = 0;
            }
        }

        private void LevelUpManager()
        {
            if (xpRemainingToNextLevel <= 0)
            {
                //TODO
                level++;
                xpRemainingToNextLevel = 10 * (int)Math.Pow(level, 2);
            }
        }

        public int GetCurrentLevel()
        {
            return level;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            String tempString = "" ;
            Vector2 fontOrigin = Vector2.Zero;

            // last XP
            if (timerXp.Enabled)
            {
                tempString = XpRaison + ": " + lastXpObtained.ToString() + " XP";
                fontOrigin = xpFont.MeasureString(tempString) / 2;
                spriteBatch.DrawString(xpFont, tempString, xpFontPosition, Color.Black, 0, fontOrigin, 1, SpriteEffects.None, 0);
            }

            // XP Combo
            if (timerCombo.Enabled && afficherCombo)
            {
                tempString = "COMBO: " + XpCombo.ToString() + " XP";
                fontOrigin = xpFont.MeasureString(tempString) / 2;
                spriteBatch.DrawString(xpFont, tempString, xpComboPosition, Color.OrangeRed, 0, fontOrigin, 1.2f, SpriteEffects.None, 0);
            }

            // XP Total
            tempString = "Total XP: " + totalXP.ToString() + " XP";
            spriteBatch.DrawString(xpFont, tempString, xpTotalPosition, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // XP remaining to next level
            tempString = "XP a farmer jusqu'au prochain level: " + xpRemainingToNextLevel.ToString() + " XP";
            spriteBatch.DrawString(xpFont, tempString, xpRemainingToNextLevelPosition, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            // Level
            tempString = "Level actuel: " + level.ToString();
            spriteBatch.DrawString(xpFont, tempString, xpLevelPosition, Color.OrangeRed, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
