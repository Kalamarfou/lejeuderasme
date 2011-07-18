using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Martingale.Mises
{
    public class Mise
    {
        public ArrayList Chiffres { get; set; }
        public int MultiplicateurDeGain { get; set; }
        public int MiseActuelle { get; set; }
        public int MiseDeDepart { get; set; }
        public int FailCount { get; set; }
        public int NbDePertes { get; set; }
        public int MultiplicateurDePertes { get; set; }

        private bool demiPognon = false;

        public Mise(ArrayList chiffres, int mise, int multiplicateurDeGain, int multiplicateurDePertes, int nbDePertes)
        {
            Chiffres = chiffres;
            MiseDeDepart = mise;
            MiseActuelle = mise;
            MultiplicateurDeGain = multiplicateurDeGain;
            MultiplicateurDePertes = multiplicateurDePertes;
            NbDePertes = nbDePertes;
        }

        public Mise()
        {

        }

        public int CalculerGain(int chiffre_sorti)
        {
            foreach (int chiffre in Chiffres)
            {
                if (chiffre == chiffre_sorti)
                {
                    return MiseActuelle * MultiplicateurDeGain;
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
            if (MultiplicateurDeGain == 2)
            {


                //si la mise et paire, on divise
                if (MiseActuelle % 2 == 0)
                {
                    return MiseActuelle / 2;
                }
                //si la mise est impaire, et qu'un demi pognon est déja stocké; on consome le demi pognon stocké et on renvoie 1
                else if (demiPognon)
                {
                    demiPognon = false;
                    return (MiseActuelle / 2) + 1;
                }
                //sinon on stocke un demi pognon, en attedant un deuxiéme
                else
                {
                    demiPognon = true;
                    return (MiseActuelle / 2);
                }
            }
            return 0;
        }


        internal virtual int GetProchaineMise()
        {
            if (FailCount > 0)
            {
                int r = MiseDeDepart;
                int nbDeDoublage= (int)(Math.Floor((double)(FailCount / NbDePertes)));
                for (int i = 0; i < nbDeDoublage; i++)
                {
                    r = r * MultiplicateurDePertes;
                }
                return r;
            }
            else
                return MiseDeDepart;
        }
    }

}
