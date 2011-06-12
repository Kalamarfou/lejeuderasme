using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimateErasme.MenuStates
{
    class PersoFinal
    {
        private static PersoFinal persoFinal;

        public bool persoValide { get; set; }
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
        public string age;
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

            String[] alignementRecommande = new String[] { "Oui-Oui", "Pokemon", "Justifié", "Parallèle", "Le poulpe", "Bourré", "Contre-utopiste", "Fifi Brindacier", "Élitiste", "Malade imaginaire", "Fourbe", "Voltaire" };
            alignement = alignementRecommande[random.Next(12)];

            String[] diviniteRecommande = new String[] { "Bulo", "Age of Empires", "Raelrasme", "King Tiger", "La panthère rose", "Le grand Bassoul", "Dieu des Kikoos", "Moustache", "Le dieu de la moule" };
            divinite = diviniteRecommande[random.Next(9)];

            String[] personnaliteRecommande = new String[] { "Skyzophrène", "Ancien Scientologue rescapé", "Humaniste", "Scientologue infiltré", "Psychopathe", "Fan des légumes", "Ami des poneys", "Mouleux" };
            personnalite = personnaliteRecommande[random.Next(8)];

            String[] prenomRecommande = new String[] { "Erasme", "Moustache", "Macédoine", "Endive", "Bulo", "Bolu", "Ulbo", "Tigre", "Coh", "Estelle", "Jolly", "Ranger", "killer", "Beau Gosse", "Canapé", "Fromage", "Vaucluse 4 LOL" };
            prenom = prenomRecommande[random.Next(17)];

            String[] nomRecommande = new String[] { "Bulo", "Bolu", "Ulbo", "Antichar", "Erasme", "Moustache", "Macédoine", "Endive", "Tigre", "Coh", "Rabelais", "Moule", "De la moule des plaines", "Dixitien"};
            nom = nomRecommande[random.Next(14)];

            age = random.Next(8, 90).ToString();
            String[] histoireRecommande = new String[] { "Orphelin, j'ai passé une bonne partie de ma vie en forêt, elevé par des singes. C'est pourquoi je n'aime pas les poneys.", "Mon père était le maire de mon village. J'en ai été bannis à huit ans quand j'ai mis le feu à sa statue. J'ai alors parcouru le monde à dos de poney. J'adore les poneys.", "Mon histoire aurait pu être semblable à celle de tout un chacun, sauf que moi, j'ai su la transcender. Partout on m'idole. Je suis le Bassoul du jeu d'Erasme.", "Cé baite mé rakonthé mon histoire cé vrémén trau konpliké. Vie dan grote. Moi aimé noir é femme.", "Bonjour, je suis très poli et très souriant. En fait, je suis prêt à vous servir. Vous voulez des artichauts ? Des noix ? De la salade ? Oui - oui, je suis légumier depuis des générations. Et j'aime ça !" };
            histoire = histoireRecommande[random.Next(5)];

            if (persoFinal != null)
            {
                persoFinal.persoValide = false;
            }
            persoValide = false;
        }

        public static PersoFinal getInstance() {
            if (persoFinal == null)
            {
                persoFinal = new PersoFinal();
            }
            return persoFinal;
        }

        public static void setInstance(PersoFinal perso)
        {
            persoFinal = perso;
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

        public void calculerCaracteristiquesRecommandees(int resteAPlacer)
        {
            int mod = resteAPlacer % 6;

            force += resteAPlacer / 6;
            constitution += resteAPlacer / 6;
            dexterite += resteAPlacer / 6;
            sagesse += resteAPlacer / 6;
            charisme += resteAPlacer / 6 + mod;
            intelligence += resteAPlacer / 6;
        }
    }
}
