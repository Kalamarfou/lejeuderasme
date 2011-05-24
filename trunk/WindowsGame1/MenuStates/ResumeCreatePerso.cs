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
            descriptions = new Dictionary<string, List<Descriptions>>();
            List<Descriptions> listeDescriptions = new List<Descriptions>();
            Descriptions description = new Descriptions("Prénom", persoFinal.prenom);
            listeDescriptions.Add(description);

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
        }

        public override void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, String choixSelect, SpriteFont font)
        {
        }

    }
}
