using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Martingale.Mises;

namespace Martingale
{
    public static class Roulette
    {
        static Random rndNumbers = new Random();

        public static int Randomiser_Chiffre()
        {
            return rndNumbers.Next(0, 36);
        }

        internal static void Jouer()
        {
            int chiffre_sorti = Roulette.Randomiser_Chiffre();
            foreach (Mise m in Data.Mises)
            {
                int gain = m.CalculerGain(chiffre_sorti);

                if (gain > 0)
                {
                    Data.Pognon += gain;
                    m.FailCount = 0;
                }
                else
                {
                    m.FailCount++;
                }
            }

            Data.HistoriqueChiffres.Add(chiffre_sorti);
            Data.HistoriquePognon.Add(Data.Pognon);
        }
    }
}
