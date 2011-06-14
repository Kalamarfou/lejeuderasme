using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Martingale.Mises
{
    class Mise
    {
        public ArrayList Chiffres { get; set; }
        public int Multiplicateur { get; set; }
        public int MiseActuelle { get; set; }
        public int MiseDeDepart { get; set; }
        public int FailCount { get; set; }

        public Mise(ArrayList chiffres, int mise, int multiplicateur)
        {
            Chiffres = chiffres;
            MiseDeDepart = mise;
            MiseActuelle = mise;
            Multiplicateur = multiplicateur;
        }

        public int CalculerGain(int chiffre_sorti)
        {
            foreach (int chiffre in Chiffres)
            {
                if (chiffre == chiffre_sorti)
                {
                    return MiseActuelle * Multiplicateur;
                }
            }
            return 0;
        }

    }

}
