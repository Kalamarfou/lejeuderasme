using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace UltimateErasme.MenuStates
{
class TypeRace : DescriptionTypes
    {
        Game game;
        public String choixFinal {get; set; }

        public TypeRace(Game game)
        {
            this.game = game;
        }

        public override void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre)
        {

            listeButtons = new List<ButtonMenu>();
            listeChoix = new List<ButtonMenu>();
            descriptions = new Dictionary<string, List<Descriptions>>();

            ButtonMenu button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Recommandé", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            List<Descriptions> ListeDescriptions = new List<Descriptions>();

            ButtonMenu choix = new ButtonMenu("Licorne", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width/10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Apparence", "Une corne magique et un texttttttttttttttttttttttttttttteeeeeeeeeeeeeeeeeeeeeeeeee supppppperrrrrrrrrrrrrrrrrrrr lonnnnnnnnnnnnnnnggggggggggggg");
            ListeDescriptions.Add(description);
            description = new Descriptions("Utilité", "Peut éclairer la nuit. A ce qu'il paraît.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Vertuchoux", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 50));
            listeChoix.Add(choix);
            description = new Descriptions("Apparence", "Un choux gigantesque");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Elfe de la Mocheté", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            description = new Descriptions("Apparence", "Juste moche.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Poney", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 150));
            listeChoix.Add(choix);
            description = new Descriptions("Apparence", "Une jolie crinière");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Erasme pur", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 200));
            listeChoix.Add(choix);
            description = new Descriptions("Phrase préférée", "Blup");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choixSelect = "Erasme pur";
            titre = "CHOIX DE LA RACE";
        }

        public override String getValeurRecommande(PersoFinal persoFinal) {
            return persoFinal.race;
        }

        public override void setValeurRecommande(PersoFinal persoFinal, String value)
        {
            persoFinal.race = value;
        }
    }

    class TypeClasse : DescriptionTypes
    {
        Game game;

        public TypeClasse(Game game)
        {
            this.game = game;
        }

        public override void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre)
        {

            listeButtons = new List<ButtonMenu>();
            listeChoix = new List<ButtonMenu>();
            descriptions = new Dictionary<string, List<Descriptions>>();

            ButtonMenu button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 550, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Recommandé", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            List<Descriptions> ListeDescriptions = new List<Descriptions>();

            ButtonMenu choix = new ButtonMenu("Le mouleux", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width /10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Avantage", "Une moule incomparable");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le gacheur", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width/10 + 50));
            listeChoix.Add(choix);
            description = new Descriptions("Avantage", "Aucun");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le jumeau", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            description = new Descriptions("Avantage", "N'a pas besoin de manger");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le paumé", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 150));
            listeChoix.Add(choix);
            description = new Descriptions("Avantage", "Des évènements inédits vous attendent");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le raleur", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 200));
            listeChoix.Add(choix);
            description = new Descriptions("Avantage", "Aucun");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choixSelect = "Le gacheur";
            titre = "CHOIX DE LA CLASSE";
        }

        public override String getValeurRecommande(PersoFinal persoFinal)
        {
            return persoFinal.classe;
        }

        public override void setValeurRecommande(PersoFinal persoFinal, String value)
        {
            persoFinal.classe = value;
        }
    }

    class TypeAlignement : DescriptionTypes
    {
        Game game;

        public TypeAlignement(Game game)
        {
            this.game = game;
        }

        public override void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre)
        {

            listeButtons = new List<ButtonMenu>();
            listeChoix = new List<ButtonMenu>();
            descriptions = new Dictionary<string, List<Descriptions>>();

            ButtonMenu button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 550, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Recommandé", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            List<Descriptions> ListeDescriptions = new List<Descriptions>();

            ButtonMenu choix = new ButtonMenu("Loyal bon", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Caractéristiques", "S'inquiète de savoir si les autres mangent à leur faim et rendra toujours une bonne faveur.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Neutre bon", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 50));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'inquiète de savoir si les autres mangent à leur faim et rendra parfois une bonne faveur.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Chaotique bon", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'inquiète de savoir si les autres mangent à leur faim mais ne rendra jamais une bonne faveur.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Loyal neutre", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 150));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'en fout un peu des autres mais rendra toujours une bonne faveur.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Neutre strict", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 200));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'en fout un peu des autres et fera en sorte de ne pas avoir de faveur à rendre.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Chaotique neutre", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 250));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'en fout un peu des autres et ne rendra jamais une bonne faveur.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Loyal mauvais", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 300));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'amuse à bruler les gens mais rend toujours les faveurs qu'on lui fait.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Neutre mauvais", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 350));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'amuse à bruler les gens mais pourra rendre les faveurs qu'on lui fait.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Chaotique mauvais", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 400));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "S'amuse à bruler les gens et ne rendra jamais une bonne faveur.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choixSelect = "Neutre strict";
            titre = "CHOIX DE L'ALIGNEMENT";
        }

        public override String getValeurRecommande(PersoFinal persoFinal)
        {
            return persoFinal.alignement;
        }

        public override void setValeurRecommande(PersoFinal persoFinal, String value)
        {
            persoFinal.alignement = value;
        }
    }

    class TypeDivinite : DescriptionTypes
    {
        Game game;

        public TypeDivinite(Game game)
        {
            this.game = game;
        }

        public override void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre)
        {

            listeButtons = new List<ButtonMenu>();
            listeChoix = new List<ButtonMenu>();
            descriptions = new Dictionary<string, List<Descriptions>>();

            ButtonMenu button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 550, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Recommandé", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            List<Descriptions> ListeDescriptions = new List<Descriptions>();

            ButtonMenu choix = new ButtonMenu("Bulo", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Représentation", "Une blouse éponge, un bec bunser dans une main et une éprouvette dans l'autre.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Rael", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 50));
            listeChoix.Add(choix);
            description = new Descriptions("Croyance", "Idéologie basée sur les extraterrestres.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le dieu de la moule", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            description = new Descriptions("Croyance", "Tout est possible, tout est réalisable. C'est le jeu de la vie.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choixSelect = "Bulo";
            titre = "CHOIX DE VOTRE DIVINITÉ";
        }

        public override String getValeurRecommande(PersoFinal persoFinal)
        {
            return persoFinal.divinite;
        }

        public override void setValeurRecommande(PersoFinal persoFinal, String value)
        {
            persoFinal.divinite = value;
        }
    }

    class TypePersonnalise : DescriptionTypes
    {
        Game game;

        public TypePersonnalise(Game game)
        {
            this.game = game;
        }

        public override void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre)
        {

            listeButtons = new List<ButtonMenu>();
            listeChoix = new List<ButtonMenu>();
            descriptions = new Dictionary<string, List<Descriptions>>();

            ButtonMenu button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 550, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Recommandé", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            List<Descriptions> ListeDescriptions = new List<Descriptions>();

            ButtonMenu choix = new ButtonMenu("Pas de fond", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Histoire", "Aucune");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Ancien Scientologue rescapé", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 50));
            listeChoix.Add(choix);
            description = new Descriptions("Histoire", "Une lutte acharnée, presque impossible, mais vous en êtes sortie, le cerveau est peu creux.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Humaniste", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            description = new Descriptions("Histoire", "Erasme pur et dur. Pas de débordement toléré et tolérable !");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Ami des poneys", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 150));
            listeChoix.Add(choix);
            description = new Descriptions("Histoire", "Oh ! Que c'est beau un poney");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Mouleux", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 200));
            listeChoix.Add(choix);
            description = new Descriptions("Histoire", "De la moule et pis c'est tout.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choixSelect = "Humaniste";
            titre = "CHOIX DE VOTRE PERSONNALITE";
        }

        public override String getValeurRecommande(PersoFinal persoFinal)
        {
            return persoFinal.personnalite;
        }

        public override void setValeurRecommande(PersoFinal persoFinal, String value)
        {
            persoFinal.personnalite = value;
        }
    }
}
