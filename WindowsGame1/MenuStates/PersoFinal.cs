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
        Random random = new Random();
        int calculDeMoule = 2;

        public PersoFinal()
        {
            //Valeur du "recommandé" : aléatoire
            String[] raceRecommande = new String[] { "Licorne", "Vertuchoux", "Elfe de la Mocheté", "Poney", "Erasme pur" };
            race = raceRecommande[random.Next(5)];

            String[] classeRecommande = new String[] { "Le mouleux", "Le gacheur", "Le jumeau", "Le paumé", "Le raleur" };
            classe = classeRecommande[random.Next(5)];

            String[] alignementRecommande = new String[] { "Loyal bon", "Neutre bon", "Chaotique bon", "Loyal neutre", "Neutre strict", "Chaotique neutre", "Loyal mauvais", "Neutre mauvais", "Chaotique mauvais" };
            alignement = alignementRecommande[random.Next(9)];
            
            String[] diviniteRecommande = new String[] { "Bulo", "Rael", "Le dieu de la moule" };
            divinite = diviniteRecommande[random.Next(3)];

            String[] personnaliteRecommande = new String[] { "Pas de fond", "Ancien Scientologue rescapé", "Humaniste", "Ami des poneys", "Mouleux" };
            personnalite = personnaliteRecommande[random.Next(5)];

            String[] prenomRecommande = new String[] { "Erasme", "Moustache", "Macédoine" };
            prenom = prenomRecommande[random.Next(3)];

            String[] nomRecommande = new String[] { "Bulo", "Bolu", "Ulbo" };
            nom = nomRecommande[random.Next(3)];

            age = random.Next(8, 90);
            String[] histoireRecommande = new String[] { "Orphelin, j'ai passé une bonne partie de ma vie en forêt, elevé par des singes. C'est pourquoi je n'aime pas les poneys.", "Mon père était le maire de mon village. J'en ai été bannis à huit ans quand j'ai mis le feu à sa statue. J'ai alors parcouru le monde à dos de poney. J'adore les poneys.", "Mon histoire aurait pu être semblable à celle de tout un chacun, sauf que moi, j'ai su la transcender. Partout on m'idole. Je suis le Bassoul du jeu d'Erasme." };
            histoire = histoireRecommande[random.Next(3)];
        }

        public void calculerForce()
        {
            if (personnalite.Equals("Mouleux")) calculDeMoule += 3;
            if (divinite.Equals("Le dieu de la moule")) calculDeMoule += 3;

            force = 8;
            if(race.Equals("Poney")) force += 2;
            if(race.Equals("Vertuchoux")) force -= 2;
            if(race.Equals("Licorne")) force -= 4;
            if(race.Equals("Elfe de la Mocheté")) force -= 2;

            if (classe.Equals("Le mouleux")) force = force + random.Next(calculDeMoule) - random.Next(calculDeMoule); //Mouleux powaaa
            if (classe.Equals("Le jumeau")) force -= 1;
            if (classe.Equals("Le paumé")) force -= 1;
        }

        public void calculerConstitution()
        {
            constitution = 8;
            if (race.Equals("Poney")) constitution += 2;
            if (race.Equals("Vertuchoux")) constitution -= 2;
            if (race.Equals("Elfe de la Mocheté")) constitution--;

            if (classe.Equals("Le mouleux")) constitution = constitution + random.Next(calculDeMoule) - random.Next(calculDeMoule); //Mouleux powaaa
            if (classe.Equals("Le jumeau")) constitution += 1;
            if (classe.Equals("Le paumé")) constitution -= 1;
        }

        public void calculerDexterite()
        {
            dexterite = 8;
            if (race.Equals("Poney")) dexterite += 2;
            if (race.Equals("Vertuchoux")) dexterite -= 2;
            if (race.Equals("Licorne")) dexterite += 1;
            if (race.Equals("Elfe de la Mocheté")) dexterite += 2;

            if (classe.Equals("Le mouleux")) dexterite = dexterite + random.Next(calculDeMoule) - random.Next(calculDeMoule); //Mouleux powaaa
            if (classe.Equals("Le paumé")) dexterite -= 1;
        }

        public void calculerSagesse()
        {
            sagesse = 8;
            if (race.Equals("Poney")) sagesse -= 3;
            if (race.Equals("Vertuchoux")) sagesse += 2;
            if (race.Equals("Licorne")) sagesse += 1;
            if (race.Equals("Elfe de la Mocheté")) sagesse += 1;

            if (classe.Equals("Le mouleux")) sagesse = sagesse + random.Next(calculDeMoule) - random.Next(calculDeMoule); //Mouleux powaaa
            if (classe.Equals("Le paumé")) sagesse += 5;
            if (classe.Equals("Le raleur")) sagesse -= 1;
        }

        public void calculerCharisme()
        {
            charisme = 8;
            if (race.Equals("Vertuchoux")) charisme += 4;
            if (race.Equals("Licorne")) charisme += 1;
            if (race.Equals("Elfe de la Mocheté")) charisme += 1;

            if (classe.Equals("Le mouleux")) charisme = charisme + random.Next(calculDeMoule) - random.Next(calculDeMoule); //Mouleux powaaa
            if (classe.Equals("Le paumé")) charisme -= 1;
            if (classe.Equals("Le raleur")) charisme += 2;
        }

        public void calculerIntelligence()
        {
            intelligence = 8;
            if (race.Equals("Poney")) intelligence -= 3;
            if (race.Equals("Licorne")) intelligence += 1;

            if (classe.Equals("Le mouleux")) intelligence = intelligence + random.Next(calculDeMoule) - random.Next(calculDeMoule); //Mouleux powaaa
            if (classe.Equals("Le paumé")) intelligence -= 1;
            if (classe.Equals("Le raleur")) intelligence -= 1;
        }

        public void calculerCaracteristiquesRecommandees()
        {
            force += 5;
            constitution += 5;
            dexterite += 5;
            sagesse += 5;
            charisme += 5;
            intelligence += 5;
        }
    }
}
