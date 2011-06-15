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
        public static int PognonDeDepart { get; set; }
        public static ArrayList Mises { get; set; }
        public static ArrayListChiffres HistoriqueChiffresGlobal { get; set; }
        public static ArrayList HistoriquePognon { get; set; }
        public static ArrayList HistoriquePognonFinal { get; set; }
        public static bool Doom { get; set; }

        public static void initSession(int pognon)
        {
            PognonDeDepart = pognon;
            Pognon = pognon;
            Mises = new ArrayList();
            HistoriquePognon = new ArrayList();
            Doom = false;
        }

        public static void initGlobal()
        {
            HistoriqueChiffresGlobal = new ArrayListChiffres();
            HistoriquePognonFinal = new ArrayList();
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

        public static string AfficherHistoriquePognonFinal()
        {
            string s = "";
            foreach (int p in Data.HistoriquePognonFinal)
            {
                s += p.ToString() + "\r\n";
            }
            return s;
        }


        public static string AfficherHistoriqueChiffresGlobal()
        {
            string s = "";
            foreach (Number n in Data.HistoriqueChiffresGlobal)
            {
                s += n.N.ToString() + "==>" +n.Occurrences.ToString()+ "\r\n";
            }
            return s;
        }

        internal static string AfficherHistoriquePognonFinalResume()
        {
            string s = "";
            double gagnant = 0;
            double gains = 0;
            double gainsMax = 0;
            double perdant = 0;
            double pertes = 0;
            double doomed = 0;
            double bug = 0;
            foreach (int pognon in HistoriquePognonFinal)
            {
                if (pognon > PognonDeDepart)
                {
                    gagnant++;
                    gains += pognon - PognonDeDepart;
                    if (pognon > gainsMax)
                    {
                        gainsMax = pognon;
                    }
                }
                else if (pognon <= PognonDeDepart)
                {
                    perdant++;
                    pertes += PognonDeDepart - pognon;
                }
                else
                {
                    bug++;
                }

                if (pognon == 0)
                {
                    doomed++;
                }
            }

            s += "gagnants : " + gagnant + "\r\n";
            s += "perdants : " + perdant + "\r\n";
            s += "  dont doomed : " + doomed + "\r\n";
            s += "buggués : " + bug + "\r\n";
            s += "\r\n";
            s += "Moyenne gains : " + gains/gagnant + "\r\n";
            s += "Moyenne pertes : " + pertes/perdant + "\r\n";
            s += "Max gains : " + gainsMax + "\r\n";
            s += "\r\n";
            s += "ratio : " + (gagnant/HistoriquePognonFinal.Count) * 100  + "%\r\n";

            return s;
        }
    }
}
