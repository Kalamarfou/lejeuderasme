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

        public ResumeCreatePerso(Game game)
        {
            this.game = game;
            persoFinal = PersoFinal.getInstance();
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
            String texte = "Vous êtes donc un " + persoFinal.race + " avec pour classe : " + persoFinal.classe + ". Vous n'avez de foi qu'en " + persoFinal.divinite + " et vous êtes " + persoFinal.personnalite + " et " + persoFinal.alignement;
            int tailleRestante = texte.Length;
            int i = 0;
            float y = game.GraphicsDevice.Viewport.Height / 10;
            while (i < texte.Length)
            {
                int tailleMax = (viewportRect.Width / 11);
                if (tailleRestante > tailleMax)
                {
                    spriteBatch.DrawString(font, texte.Substring(i, tailleMax), new Vector2(viewportRect.X, y + 20), Color.DarkBlue);
                    tailleRestante -= tailleMax;
                    i += tailleMax;
                }
                else
                {
                    spriteBatch.DrawString(font, texte.Substring(i, tailleRestante), new Vector2(viewportRect.X, y + 20), Color.DarkBlue);
                    i += tailleRestante;
                    tailleRestante = 0;
                }
                y += 20;
            }
            y += 40;
        }

        public override void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
        {
        }

    }
}
