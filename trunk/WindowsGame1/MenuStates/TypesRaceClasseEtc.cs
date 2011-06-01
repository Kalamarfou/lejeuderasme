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

            ButtonMenu choix = new ButtonMenu("Licorne", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height /10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Apparence", "La licorne porte une corne au sommet de son front qui possède selon certains légendes de multiples pouvoirs dont celui de lutter contre le poison.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "La licorne ne brille pas par sa force, mais par toutes ces autres caractéristiques.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Particularité de la race", "Les licornes ont pour habitude de vivre la nuit, cachées. Elles ne sont pas de mauvaises natures, bien au contraire. Mais elles ont tendance à se méfier des autres.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Vertuchoux", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height / 10 + 50));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Apparence", "Le Vertuchoux porte un brocolis sur la tête. Un brocolis que la plupart regarde avec respect et admiration.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Le vertuchoux brille par sa sagesse et son charisme.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Particularité de la race", "Le vertuchoux est un être un peu perdu dans le temps qui ne manque jamais ouvertement de respect à quelqu'un.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Elfe de la Mocheté", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height / 10 + 100));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Apparence", "Juste moche.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "L'elfe est un être agile et dôté de bonnes capacités intellectuelles.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Particularité de la race", "L'elfe de la mocheté se cache en fôrêt. Il ne rencontre jamais personne car il a honte ... Oui, il est vraiment moche.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Poney", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height / 10 + 150));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Apparence", "Une crinière symbole de fougue et de manque de naiveté.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Le poney est bête comme ces pieds, mais est une sacrée brute physique.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Particularité de la race", "Le poney vit au milieu de tous. Oui, lui n'a pas honte de ce qu'il est. Au contraire, il est fier comme un pou.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Erasme pur", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Height / 10 + 200));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Apparence", "Une perfection de géométrie. Retennant l'admiration de tous.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "L'érasme pur est très équilibré dans toutes ces caractéristiques.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Particularité de la race", "L'érasme pur ne vit que pour entretenir sa graisse et a pour phrase préférée : 'blup'");
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
            Descriptions description = new Descriptions("Particularités", "Le mouleux est capable de tout. La règle d'or face à un mouleux est de ne jamais douter de la possibilité de l'impossible.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Entraînement", "Aucun.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le gacheur", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width/10 + 50));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Particularités", "Classe par défaut de part sa prépondérance, le gacheur ultime est quand même loin de la bassesse de la société. Ne tendez JAMAIS votre équipement en direction d'un gacheur.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Entraînements", "Les entraînements du gacheur sont multiples et quotidiens. Le gacheur ne doit jamais laissé passer une occasion. Un gateau en chocolat, une allumette ou un verre de lait ... De multiples gachages en vues.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le jumeau", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Particularités", "Le jumeau n'a pas besoin de manger, ou très peu, ce qui en fait un redoutable aventurier. Par contre, le jumeau a un retard technologique impressionnant. L'écran bleu est sa passion.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Entraînements", "Son entraînement consiste à faire régulièrement des crêpes et à ne payer les barbecues qu'une semaine plus tard, au moins.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le paumé", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 150));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Particularités", "Le paumé ne réfléchit jamais ce qui lui permet d'avoir l'avantage de vivre des évènements inédits. Là ou d'autres pourront ne prendre qu'à droite ou à gauche, vous vous pourrez aussi faire demi-tour.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Entraînements", "Le paumé est souvent un artiste incompris qui passe ces journées à peindre ou à scuplter.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le raleur", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 200));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Particularités", "Le raleur a la puissance d'un tigre. De part son charisme, il est capable d'immobiliser une voiture et de faire arrêter celle-ci où il veut. A la boulangerie ? Au ski ? Aucun problème. Il décide du début de la lan et rejoint les parties toujours en dernier. Pourtant, c'est lui qui décide du jeu et des paramètres de la partie. Et des équipes aussi.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Entraînements", "Il s'entraîne souvent en choisissant l'une des pires décisions et en essayant de l'imposer aux autres et à lui-même. Comme cela, il a encore plus envie de raler.");
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

            ButtonMenu choix = new ButtonMenu("Oui-Oui", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 -20));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Caractéristiques", "Vous êtes vraiment l'ami de tous. Vous ne faites aucune différence entre un poney et une licorne. Tous les deux méritent tout autant votre amour débordant.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Pokemon", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 20));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Juste Pokemon.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Justifié", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 60));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "A droite, à gauche, au centre ? Non, JAMAIS. Justifié c'est mieux.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Parallèle", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Parallèle, mais parallèle à quoi ? Telle est la question.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le poulpe", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 140));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Vous n'avez pas d'alignement. Vous vous laissez guider par les courants tel un immense poulpe.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Bourré", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 180));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Heu ... Vous vous avez vraiment des problèmes d'alignement. Marchez droit, au moins un mètre ? Non, ça vous dit pas ?");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Contre-utopiste", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 220));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Pessimiste et moqueur, telles sont les qualités que vous visez.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Fifi Brindacier", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 260));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Oh mon dieu ! Descendez de ce caniche !");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);
            
            choix = new ButtonMenu("Élitiste", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 300));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Tiens, il y a des fourmis en bas, non ? Ah non ... il y avait des fourmis. Votre objectif est d'anhihiler la médiocrité de ce monde. Pas facile.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Malade imaginaire", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 340));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Il en est comme de ces beaux songes qui ne vous laissent au réveil que le déplaisir de les avoir crus.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Fourbe", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 380));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Moi je voulais dire, vous... vous vous êtes vraiment très gentil.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Voltaire", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 420));
            listeChoix.Add(choix);
            description = new Descriptions("Caractéristiques", "Ahh ! Ne prononcez pas ce nom aussi fort. Vous êtes le mal en personne.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choixSelect = "Le poulpe";
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

            ButtonMenu choix = new ButtonMenu("La panthère rose", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 - 20));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Dénomination", "Dieu du Rose et de l'Amour.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Cette croyance explique la création de la planète et la naissance de l'humanité par un surexès d'amour de la part de la panthère rose. Son dieu est bon, son dieu est amour, son dieu est gné.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Tout comme la panthère rose.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Oh le rose ! Mettez du rose ! C'est trop beau");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Age of Empires", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 20));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu des fermes, des monastères, des paladins et des mauvais PC.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Cette croyance explique que chaque fois qu'un joueur crée un profil, un monde associé se crée automatiquement et est sous la responsabilité du nouveau joueur.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Un immense moine d'Age of Empires II");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Même ma grand-mère peut faire mieux.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Moustache", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 60));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu des moustaches.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Cette croyance explique que chaque moustache a son identité propre et transcende un homme ou une femme, mais celle-ci doit être mérité par un long périple. Le pélerinage de la moustache.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Un homme un peu rectangle avec une super giga moustache.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Voir la vie en moustache, c'est voir la vie du bon côté. Tout simplement.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le dieu de la moule", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 100));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu de la moule");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "300 grammes de moule au micro-ondes, on remue bien et hop, de la supermoule !");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Cette divinité à l'apparence d'un kalamar.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Tout est possible, tout est réalisable. C'est le jeu de la vie.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Bulo", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 140));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu des molécules, de l'adn, des moles et de la subition.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Elle explique que le nombre d'Avogadro est à la base du monde. 200 fois le nb d'Avogadro peut représenter un être intelligent.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Une blouse éponge, un bec bunser dans une main et une éprouvette dans l'autre.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Faites vos DM, imprégnez vous d'acide bananique. Prenez une éponge de 2,3 diméthyl 3,4 dipropan2-ol et en avant !");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Raelrasme", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 180));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu de l'extraordinaire");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Elle explique qu'avant l'être humain était une sorte de boite avec écrit 'Gné' dessus. Bizarre. Cependant, un rayon extraterresque a un jour aspiré toutes les boites gné de manière à les transférer dans le vaisseau de Raelrasme. C'est lui qui a éveillé la vie par ces pouvoirs.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Une barbe blanche, des vêtements blancs. Un vrai gourou.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Tout n'est qu'un et tout n'est qu'extraordinaire.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Dieu des Kikoos", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 220));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu des Kikoos et des lols.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Cette croyance explique que les jeunes c'est mieux et que tout le reste c'est ringard.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Des cheveux empâlés par le gel et des lunettes de soleil.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "KikOouu mon petit fan !! Rajoutes lol à la fin de tes phrases. C'est plus beau gosse, lol. MDR LOL.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Le grand Bassoul", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 260));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu du feu");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Cette croyance n'explique rien. Toutefois, elle recommande de tout brûler... dans le doute.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Un immense briquet dans une main, une boite d'allumette dans l'autre et un super sourire sur le visage.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Tout brûlerrrr ! Craaame charogne ! (Ah ! zut ! C'était ma chambre.)");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("King Tiger", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 300));
            listeChoix.Add(choix);
            ListeDescriptions = new List<Descriptions>();
            description = new Descriptions("Dénomination", "Dieu de la puissance, de la force et de la destruction.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Caractéristiques", "Cette croyance explique que l'objectif d'une vie et de trouver adversaire à sa taille.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Apparence", "Un tank immense plus effroyable que ce que l'humanité a pu imaginer en le créant.");
            ListeDescriptions.Add(description);
            description = new Descriptions("Philosophie", "Bon, pour se battre à la loyale, je prends le front droit et vous tanks minables, prennez l'autre front. Oh! Et puis non. Attendez moi là.");
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

            ButtonMenu choix = new ButtonMenu("Skyzophrène", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10));
            listeChoix.Add(choix);
            Descriptions description = new Descriptions("Particularités", "Vous êtes capables de jouer tout seul pendant des heures. Tout seul ... Ca c'est ce que certains croient ! Ces idiots. Ils ne savent pas de toute façon, ce ne sont pas la réalité, eux. Ce sont comme des boites, des boites qui sonnent creux. Alors que vous, vous sonnez juste. Ouai, c'est les autres ne sont que des fantômes, des êtres sans vies. Heureusement que vous êtes là, que dieu vous ait envoyé du futur pour tous les sauver.");
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Ancien Scientologue rescapé", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 40));
            listeChoix.Add(choix);
            description = new Descriptions("Particularités", "Que dire ? Vous n'avez plus d'argent. Vos chiens et vos chats sont encore là bas, du côté des scientologues. Mais vous, vous n'avez pas abandonné, jamais vous n'avez laché. A croire que vous n'avez supporté l'idée de ne jamais pouvoir jouer à battlefield 3. Bravo ! Vous avez du caractère, vous ! Pourtant, depuis votre épreuve, beaucoup prétendent que votre cerveau sonne bizarrement creux."); 
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Humaniste", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 110));
            listeChoix.Add(choix);
            description = new Descriptions("Particularités", "Erasme pur et dur. Pas de débordement toléré et tolérable ! Vous vous imaginez, ce serait comme prendre un radis avec des tomates. On ne fait pas ces courses avec l'histoire, monsieur !");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Scientologue infiltré", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 150));
            listeChoix.Add(choix);
            description = new Descriptions("Particularités", "Méfiez-vous. Je vous invite à vous méfiez. Peut être pensez-vous être dans un monde joyeux, mais ce n'est pas le cas. Ouvrez les yeux ! Regardez ces enfants qui meurent aux informations et regardez autour de vous ! Tous ces gens qui vous approche comme des sangsues ! Ils vous en veulent c'est évident. Laissez-moi, laissez-moi gêrer tout ça et rester bien au calme chez-vous. Écoutez c'est simple. Vous préférez quoi ... Que des enfants meurent du sida ou alors vous préféreriez signer ce minuscule document ?");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Ami des poneys", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 200));
            listeChoix.Add(choix);
            description = new Descriptions("Particularités", "Les poneys c'est vraiment bien. Moi, si on devait m'obliger à choisir entre un poney et une endive, je pense que je me suiciderais tellement le choix est diffile. Les poneys, c'est vraiment vraiment trop trop jolie. Oh ! Et puis vous avez vu, il vient de bouger ! Hihi !");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Mouleux", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 250));
            listeChoix.Add(choix);
            description = new Descriptions("Particularités", "Trèves de bavardages. Je veux jouer. Donnez moi un dé et un lit. Moi j'en ai marre de ces gens qui réfléchissent aux stats pendant des heures. Les choix dans la vie c'est de la moule ... et pis c'est tout.");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Psychopathe", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 300));
            listeChoix.Add(choix);
            description = new Descriptions("Particularités", "ANTISOCIAL ! Ouai les autres c'est qu'un groupe de moutons idiots ! Faut les brûler !! TOUS ! Faut les châtier, leur arracher leurs ongles, les entendre hurler ! Leur pisser dessus ! Ouai ! Faut qu'ils meurent ! TOUS !");
            ListeDescriptions = new List<Descriptions>();
            ListeDescriptions.Add(description);
            descriptions.Add(choix.getText(), ListeDescriptions);

            choix = new ButtonMenu("Fan des légumes", Color.DarkBlue, Color.DarkGreen, new Vector2(3 * game.GraphicsDevice.Viewport.Width / 8, game.GraphicsDevice.Viewport.Width / 10 + 350));
            listeChoix.Add(choix);
            description = new Descriptions("Particularités", "Excellent ! C'est le meilleur jour de ma vie. Vous connaissez la nouvelle ? NON ! Comment ça vous n'êtes pas abonné aux newsletters de légumes.com ? Bon je vais me répéter, mais aujourd'hui c'est le meilleur jour de ma vie. Pourquoi ? Eh bien, parce que Super U fait une réduction exceptionnelle pour trois tomates achetées ... une endive offerte !");
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
