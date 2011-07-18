using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Martingale.Mises
{
    public class MiseAdditionee : Mise
    {

        public MiseAdditionee(ArrayList chiffres, int mise, int multiplicateurDeGain, int multiplicateurDePertes, int nbDePertes)
        {
            base.Chiffres = chiffres;
            base.MiseDeDepart = mise;
            base.MiseActuelle = mise;
            base.MultiplicateurDeGain = multiplicateurDeGain;
            base.MultiplicateurDePertes = multiplicateurDePertes;
            base.NbDePertes = nbDePertes;
        }


        internal override int GetProchaineMise()
        {
            if (FailCount > 0)
            {
                int r = MiseDeDepart;
                int nbDeAdditionages = (int)(Math.Floor((double)(FailCount / NbDePertes)));
                for (int i = 0; i < nbDeAdditionages; i++)
                {
                    r = r + MultiplicateurDePertes;
                }
                return r;
            }
            else
                return MiseDeDepart;
        }
    }
}
