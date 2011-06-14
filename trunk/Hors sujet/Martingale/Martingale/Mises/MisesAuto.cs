using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Martingale.Mises
{
    public static class MisesAuto
    {
        public static void Pairs()
        {
            ArrayList c = new ArrayList();
            for (int i = 1; i < 37; i++)
            {
                if ((i % 2) == 0)
                {
                    c.Add(i);
                }
            }
            Data.Mises.Add(new Mise(c, 1, 2));
            Data.Pognon -= 1;
        }

        public static void Passe()
        {
        }

        internal static void ReMiser()
        {
            //si on a plus de fric, on perd
            if (Data.Pognon <= 0)
            {
                Data.Doom = true;
                return;
            }

            foreach (Mise m in Data.Mises)
            {
                int prochaineMise;
                if (m.FailCount > 0)
                {
                    m.FailCount++;
                    prochaineMise = m.MiseActuelle * 2;
                }
                else
                {
                    prochaineMise = m.MiseDeDepart;
                }

                if (Data.Pognon - prochaineMise < 0)
                {
                    prochaineMise = Data.Pognon;
                }
                m.MiseActuelle = prochaineMise;
                Data.Pognon -= prochaineMise;
                
            }
        }
    }
}
