using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimateErasme.MenuStates
{
    class PersoFinal
    {
        public String race { get; set; }
        public String classe { get; set; }
        public String alignement { get; set; }
        public String divinite { get; set; }

        public int force { get; set; }
        public int constitution { get; set; }
        public int dexterite { get; set; }
        public int intelligence { get; set; }
        public int sagesse { get; set; }
        public int charisme { get; set; }

        public String personnalite;
        public String prenom;
        public String nom;
        public int age;
        public String histoire;
        public String voix;

        public PersoFinal()
        {
            //Valeur du "recommandé" : aléatoire

            Random random = new Random();
            String[] raceRecommande = new String[] { "Licorne", "Vertuchoux", "Elfe de la Mocheté", "Poney", "Erasme pur" };
            race = raceRecommande[random.Next(5)];

            String[] classeRecommande = new String[] { "Le mouleux", "Le gacheur", "Le jumeau", "Le paumé", "Le raleur" };
            classe = classeRecommande[random.Next(5)];    
        }


    }
}
