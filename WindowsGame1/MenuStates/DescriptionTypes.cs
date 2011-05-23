using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace UltimateErasme.MenuStates
{
    abstract class DescriptionTypes
    {
        public abstract void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre);
        public abstract String getValeurRecommande(PersoFinal persoFinal);
        public abstract void setValeurRecommande(PersoFinal persoFinal, String value);
    }

    class TypeRace : DescriptionTypes
    {
        Game game;
        public String choixFinal {get; set; } //TODO ?

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
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 550, (game.GraphicsDevice.Viewport.Height) - 40));
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
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 550, (game.GraphicsDevice.Viewport.Height) - 40));
            listeButtons.Add(button);
            button = new ButtonMenu("Recommandé", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            List<Descriptions> ListeDescriptions = new List<Descriptions>();

            ButtonMenu choix = new ButtonMenu("Loyal bon", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Avantage", "Une moule incomparable");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le gacheur", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 50));
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
}
