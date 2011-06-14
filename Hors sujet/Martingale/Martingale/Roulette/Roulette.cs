using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Martingale
{
    public static class Roulette
    {
        static Random rndNumbers = new Random();

        public static int Randomiser_Chiffre()
        {
            return rndNumbers.Next(0, 36);
        }
    }
}
