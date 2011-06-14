using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Martingale
{
    public static class Data
    {
        public static  int Pognon { get; set; }
        public static ArrayList Mises { get; set; }
        public static ArrayList HistoriqueChiffres { get; set; }
        public static ArrayList HistoriquePognon { get; set; }
        public static bool Doom { get; set; }

        public static void init(int pognon)
        {
            Pognon = pognon;
            Mises = new ArrayList();
            HistoriqueChiffres = new ArrayList();
            HistoriquePognon = new ArrayList();
            Doom = false;
        }

        public static string AfficherHistoriquePognon()
        {
            string s = "";
            foreach (int p in Data.HistoriquePognon)
            {
                s += p.ToString() + "\r\n";
            }
            return s;
        }

        public static string AfficherHistoriqueChiffres()
        {
            string s = "";
            foreach (int c in Data.HistoriqueChiffres)
            {
                s += c.ToString() + "\r\n";
            }
            return s;
        }
    }
}
