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

            foreach (Mise m in Data.Mises)
            {
                if (m.FailCount > 0)
                {
                    m.MiseActuelle = m.MiseActuelle * 2;
                    //la mise actuelle es tdéja multipliée par deux
                    Data.Pognon -= m.MiseActuelle;
                    m.FailCount++;
                }
                else
                {
                    m.MiseActuelle = m.MiseDeDepart;
                    Data.Pognon -= m.MiseDeDepart;
                }
            }
        }
    }
}
