using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;

namespace UltimateErasme.MenuStates
{
    class ResumeCreatePerso : DescriptionTypes
    {
        Game game;
        PersoFinal persoFinal;
        Rectangle rectResume;

        public ResumeCreatePerso(Game game)
        {
            this.game = game;
            persoFinal = PersoFinal.getInstance();
            rectResume = new Rectangle(300, 100, 500, 600);
        }

        public override void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre)
        {
            listeButtons = new List<ButtonMenu>();
            ButtonMenu button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Valider", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            listeChoix = null;
            descriptions = null;

            choixSelect = "résumé";
            titre = "RÉSUMÉ DE VOTRE PERSONNAGE";

        }

        public override String getValeurRecommande(PersoFinal persoFinal)
        {
            return null;
        }

        public override void setValeurRecommande(PersoFinal persoFinal, String value)
        {
            
        }


        public override void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch, Game game, Dictionary<String, List<Descriptions>> descriptions, SpriteFont font)
        {
            float y = rectResume.Y;
            String texte = "Vous êtes donc un " + persoFinal.race + " avec pour classe : " + persoFinal.classe + ". Vous n'avez de foi qu'en " + persoFinal.divinite + " et vous êtes de personalité " + persoFinal.personnalite + " et d'alignement " + persoFinal.alignement + ". ";
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y);
            texte = "Vous vous appelez : " + persoFinal.prenom + " " + persoFinal.nom + ". Votre âge est : " + persoFinal.age + " ans. Voici votre histoire : " + persoFinal.histoire;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 50);
            texte = "FORCE : " + persoFinal.force;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 50);
            texte = "DEXTÉRITÉ : " + persoFinal.dexterite;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "CONSTITUTION : " + persoFinal.constitution;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "INTELLIGENCE : " + persoFinal.intelligence;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "SAGESSE : " + persoFinal.sagesse;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
            texte = "CHARISME : " + persoFinal.charisme;
            y = ErasmeUtils.afficherTexte(texte, game, rectResume, spriteBatch, font, Color.DarkBlue, y + 30);
        }

        public override void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
        {
        }

    }
}
