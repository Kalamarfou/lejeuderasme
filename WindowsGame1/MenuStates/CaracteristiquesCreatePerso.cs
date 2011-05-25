using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;

namespace UltimateErasme.MenuStates
{
    class CaracteristiquesCreatePerso : DescriptionTypes
    {
        Game game;
        PersoFinal persoFinal;
        int force;
        int constitution;
        int dexterite;
        int charisme;
        int intelligence;
        int sagesse;
        int resteAPlacer;
        ButtonMenu forPlus;
        ButtonMenu forMoins;
        ButtonMenu dextPlus;
        ButtonMenu dextMoins;
        ButtonMenu conPlus;
        ButtonMenu conMoins;
        ButtonMenu intPlus;
        ButtonMenu intMoins;
        ButtonMenu sagPlus;
        ButtonMenu sagMoins;
        ButtonMenu chaPlus;
        ButtonMenu chaMoins;


        public CaracteristiquesCreatePerso(Game game)
        {
            this.game = game;
            persoFinal = PersoFinal.getInstance();
            resteAPlacer = 30;
        }

        public override void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre)
        {
            listeButtons = new List<ButtonMenu>();

            ButtonMenu button = new ButtonMenu("Annuler", Color.DarkBlue, Color.DarkGreen, new Vector2(10, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Retour", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 550, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Recommandé", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 350, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);
            button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
            listeButtons.Add(button);

            listeChoix = null;
            descriptions = null;

            persoFinal.calculerSagesse();
            persoFinal.calculerCharisme();
            persoFinal.calculerIntelligence();
            persoFinal.calculerDexterite();
            persoFinal.calculerForce();
            persoFinal.calculerConstitution();

            force = persoFinal.force;
            charisme = persoFinal.charisme;
            intelligence = persoFinal.intelligence;
            dexterite = persoFinal.dexterite;
            constitution = persoFinal.constitution;
            sagesse = persoFinal.sagesse;

            forPlus = new ButtonMenu("+", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            forMoins = new ButtonMenu("-", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            dextPlus = new ButtonMenu("+", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            dextMoins = new ButtonMenu("-", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            conPlus = new ButtonMenu("+", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            conMoins = new ButtonMenu("-", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            intPlus = new ButtonMenu("+", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            intMoins = new ButtonMenu("-", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            sagPlus = new ButtonMenu("+", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            sagMoins = new ButtonMenu("-", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            chaPlus = new ButtonMenu("+", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));
            chaMoins = new ButtonMenu("-", Color.DarkBlue, Color.DarkRed, new Vector2(0, 0));

            choixSelect = null;
            titre = "CARACTÉRISTIQUES DE VOTRE PERSONNAGE";

        }

        public override String getValeurRecommande(PersoFinal persoFinal)
        {
            persoFinal.calculerCaracteristiquesRecommandees(resteAPlacer);
            force = persoFinal.force;
            constitution = persoFinal.constitution;
            dexterite = persoFinal.dexterite;
            sagesse = persoFinal.sagesse;
            intelligence = persoFinal.intelligence;
            charisme = persoFinal.charisme;

            return null;
        }

        public override void setValeurRecommande(PersoFinal persoFinal, String value)
        {
            persoFinal.force = force;
            persoFinal.charisme = charisme;
            persoFinal.constitution = constitution;
            persoFinal.intelligence = intelligence;
            persoFinal.dexterite = dexterite;
            persoFinal.sagesse = sagesse;
        }


        public override void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch, Game game, Dictionary<String, List<Descriptions>> descriptions, SpriteFont font)
        {
            
        }

        public override void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
        {
            float y = viewportRect.Y;
            Rectangle viewportRectCarac = new Rectangle(viewportRect.X, viewportRect.Y, 2 * viewportRect.Width / 3, viewportRect.Height);
            Rectangle viewportRectPlus = new Rectangle(viewportRect.X + 2 * viewportRect.Width / 3, viewportRect.Y, viewportRect.Width / 3, viewportRect.Height);
            Rectangle viewportRectMoins = new Rectangle(viewportRect.X + 2 * viewportRect.Width / 3 + 1, viewportRect.Y, viewportRect.Width / 3 - 1, viewportRect.Height);
            y = CreatePersoMenuState.afficherTexte("Reste : " + resteAPlacer, game, viewportRect, spriteBatch, font, Color.DarkBlue, y);
            y += 20;

            CreatePersoMenuState.afficherTexte("Force : " + force, game, viewportRectCarac, spriteBatch, font, Color.DarkBlue, y);
            forPlus.setX(viewportRectPlus.X);
            forPlus.setY(y-10);
            forMoins.setX(viewportRectMoins.X);
            forMoins.setY(y+10);
            if(forPlus.isNear(5)) {
                CreatePersoMenuState.afficherTexte("+", game, viewportRectPlus, spriteBatch, font, forPlus.getOnClickColor(), y - 10);
            } else {
                CreatePersoMenuState.afficherTexte("+", game, viewportRectPlus, spriteBatch, font, forPlus.getColor(), y - 10);
            }
            if (forMoins.isNear(5))
            {
                y = CreatePersoMenuState.afficherTexte("-", game, viewportRectMoins, spriteBatch, font, forMoins.getOnClickColor(), y + 10);
            }
            else
            {
                y = CreatePersoMenuState.afficherTexte("-", game, viewportRectMoins, spriteBatch, font, forMoins.getColor(), y + 10);
            }

            y += 20;
            /*CreatePersoMenuState.afficherTexte("Dextérité : " + dexterite, game, viewportRectCarac, spriteBatch, font, Color.DarkBlue, y);
            dextPlus.setX(viewportRectPlusMoins.X);
            dextPlus.setY(y - 10);
            dextMoins.setX(viewportRectPlusMoins.X);
            dextMoins.setY(y + 10);
            CreatePersoMenuState.afficherTexte("+", game, viewportRectPlusMoins, spriteBatch, font, Color.DarkBlue, y - 10);
            y = CreatePersoMenuState.afficherTexte("-", game, viewportRectPlusMoins, spriteBatch, font, Color.DarkBlue, y + 10);

            y += 20;
            CreatePersoMenuState.afficherTexte("Constitution : " + constitution, game, viewportRectCarac, spriteBatch, font, Color.DarkBlue, y);
            conPlus.setX(viewportRectPlusMoins.X);
            conPlus.setY(y - 10);
            conMoins.setX(viewportRectPlusMoins.X);
            conMoins.setY(y + 10);
            CreatePersoMenuState.afficherTexte("+", game, viewportRectPlusMoins, spriteBatch, font, Color.DarkBlue, y - 10);
            y = CreatePersoMenuState.afficherTexte("-", game, viewportRectPlusMoins, spriteBatch, font, Color.DarkBlue, y + 10);*/
        }

    }
}
