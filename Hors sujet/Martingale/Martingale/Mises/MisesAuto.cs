﻿using System;
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
            Data.Mises.Add(new Mise(c, 1, 2,2,1));
        }

        public static void Passe()
        {
            ArrayList c = new ArrayList();
            for (int i = 1; i < 19; i++)
            {
                c.Add(i);
            }
            Data.Mises.Add(new Mise(c, 1, 2, 2, 1));
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
                int prochaineMise = m.GetProchaineMise();

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