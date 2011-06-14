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

        private bool demiPognon = false;

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
                else if (chiffre_sorti == 0)
                {
                    return GererZero();
                }
            }
            return 0;
        }

        //si un zéro est tiré, on divise la mise par 2
        private int GererZero()
        {
            //si la mise et paire, on divise
            if (MiseActuelle > 1)
            {
                return MiseActuelle / 2;
            }
            //si la mise est impaire, et qu'un demi pognon est déja stocké; on consome le demi pognon stocké et on renvoie 1
            else if (demiPognon)
            {
                demiPognon = false;
                return 1;
            }
            //sinon on stocke un demi pognon, en attedant un deuxiéme
            else
            {
                demiPognon = true;
                return 0;
            }
        }

    }

}
