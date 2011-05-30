using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using UltimateErasme.GameObjects;
using System.Threading;

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

        int forceInit;
        int constitutionInit;
        int dexteriteInit;
        int charismeInit;
        int intelligenceInit;
        int sagesseInit;


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

            forceInit = persoFinal.force;
            sagesseInit = persoFinal.sagesse;
            intelligenceInit = persoFinal.intelligence;
            dexteriteInit = persoFinal.dexterite;
            constitutionInit = persoFinal.constitution;
            charismeInit = persoFinal.charisme;

        }

        public override String getValeurRecommande(PersoFinal persoFinal)
        {
            persoFinal.force = force;
            persoFinal.charisme = charisme;
            persoFinal.constitution = constitution;
            persoFinal.intelligence = intelligence;
            persoFinal.dexterite = dexterite;
            persoFinal.sagesse = sagesse;

            persoFinal.calculerCaracteristiquesRecommandees(resteAPlacer);
            force = persoFinal.force;
            constitution = persoFinal.constitution;
            dexterite = persoFinal.dexterite;
            sagesse = persoFinal.sagesse;
            intelligence = persoFinal.intelligence;
            charisme = persoFinal.charisme;

            resteAPlacer = 0;
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

        public override void changeCaracValue()
        {
            List<ButtonMenu> listeButtons = new List<ButtonMenu>();
            listeButtons.Add(forPlus);
            listeButtons.Add(forMoins);
            listeButtons.Add(dextPlus);
            listeButtons.Add(dextMoins);
            listeButtons.Add(conPlus);
            listeButtons.Add(conMoins);
            listeButtons.Add(intPlus);
            listeButtons.Add(intMoins);
            listeButtons.Add(sagPlus);
            listeButtons.Add(sagMoins);
            listeButtons.Add(chaPlus);
            listeButtons.Add(chaMoins);

            foreach (ButtonMenu button in listeButtons)
            {
                if (button.isPressed(5))
                {

                    Thread.Sleep(300);
                    if (button.getText().Equals("+") && button.getX().Equals(forPlus.getX()) && button.getY().Equals(forPlus.getY()) && resteAPlacer > 0)
                    {
                        resteAPlacer--;
                        force++;
                    }
                    else if (button.getText().Equals("+") && button.getX().Equals(conPlus.getX()) && button.getY().Equals(conPlus.getY()) && resteAPlacer > 0)
                    {
                        resteAPlacer--;
                        constitution++;
                    }
                    else if (button.getText().Equals("+") && button.getX().Equals(dextPlus.getX()) && button.getY().Equals(dextPlus.getY()) && resteAPlacer > 0)
                    {
                        resteAPlacer--;
                        dexterite++;
                    }
                    else if (button.getText().Equals("+") && button.getX().Equals(sagPlus.getX()) && button.getY().Equals(sagPlus.getY()) && resteAPlacer > 0)
                    {
                        resteAPlacer--;
                        sagesse++;
                    }
                    else if (button.getText().Equals("+") && button.getX().Equals(chaPlus.getX()) && button.getY().Equals(chaPlus.getY()) && resteAPlacer > 0)
                    {
                        resteAPlacer--;
                        charisme++;
                    }
                    else if (button.getText().Equals("+") && button.getX().Equals(intPlus.getX()) && button.getY().Equals(intPlus.getY()) && resteAPlacer > 0)
                    {
                        resteAPlacer--;
                        intelligence++;
                    }
                    else if (button.getText().Equals("-") && button.getX().Equals(forMoins.getX()) && button.getY().Equals(forMoins.getY()) && force > forceInit)
                    {
                        resteAPlacer++;
                        force--;
                    }
                    else if (button.getText().Equals("-") && button.getX().Equals(dextMoins.getX()) && button.getY().Equals(dextMoins.getY()) && dexterite > dexteriteInit)
                    {
                        resteAPlacer++;
                        dexterite--;
                    }
                    else if (button.getText().Equals("-") && button.getX().Equals(conMoins.getX()) && button.getY().Equals(conMoins.getY()) && constitution > constitutionInit)
                    {
                        resteAPlacer++;
                        constitution--;
                    }
                    else if (button.getText().Equals("-") && button.getX().Equals(intMoins.getX()) && button.getY().Equals(intMoins.getY()) && intelligence > intelligenceInit)
                    {
                        resteAPlacer++;
                        intelligence--;
                    }
                    else if (button.getText().Equals("-") && button.getX().Equals(sagMoins.getX()) && button.getY().Equals(sagMoins.getY()) && sagesse > sagesseInit)
                    {
                        resteAPlacer++;
                        sagesse--;
                    }
                    else if (button.getText().Equals("-") && button.getX().Equals(chaMoins.getX()) && button.getY().Equals(chaMoins.getY()) && charisme > charismeInit)
                    {
                        resteAPlacer++;
                        charisme--;
                    }
                }
            }
        }

        public override void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch, Game game, Dictionary<String, List<Descriptions>> descriptions, SpriteFont font)
        {
            String texte = "La force c'est la force. La dextérité c'est la vitesse, l'agilité. Le charisme peut vous permettre de réussir vos ralages, tout comme l'intelligence vous aide à gacher. En tant que mouleux, vous ne devez pas hésiter à cliquer sur ... recommandé.";
            CreatePersoMenuState.afficherTexte(texte, game, viewportRect, spriteBatch, font, Color.DarkBlue, viewportRect.Y);
        }

        public override void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
        {
            float y = viewportRect.Y;
            Rectangle viewportRectCarac = new Rectangle(viewportRect.X, viewportRect.Y, 2 * viewportRect.Width / 3, viewportRect.Height);
            Rectangle viewportRectPlus = new Rectangle(viewportRect.X + 2 * viewportRect.Width / 3, viewportRect.Y, viewportRect.Width / 3, viewportRect.Height);
            Rectangle viewportRectMoins = new Rectangle(viewportRect.X + 2 * viewportRect.Width / 3, viewportRect.Y, viewportRect.Width / 3, viewportRect.Height);
            y = CreatePersoMenuState.afficherTexte("Reste : " + resteAPlacer, game, viewportRect, spriteBatch, font, Color.DarkBlue, y);
            y += 30;

            y = afficherChoix(spriteBatch, font, viewportRectCarac, viewportRectPlus, viewportRectMoins, "Force", force, forPlus, forMoins, y);
            y += 30;
            y = afficherChoix(spriteBatch, font, viewportRectCarac, viewportRectPlus, viewportRectMoins, "Dextérité", dexterite, dextPlus, dextMoins, y);
            y += 30;
            y = afficherChoix(spriteBatch, font, viewportRectCarac, viewportRectPlus, viewportRectMoins, "Constitution", constitution, conPlus, conMoins, y);
            y += 30;
            y = afficherChoix(spriteBatch, font, viewportRectCarac, viewportRectPlus, viewportRectMoins, "Intelligence", intelligence, intPlus, intMoins, y);
            y += 30;
            y = afficherChoix(spriteBatch, font, viewportRectCarac, viewportRectPlus, viewportRectMoins, "Sagesse", sagesse, sagPlus, sagMoins, y);
            y += 30;
            y = afficherChoix(spriteBatch, font, viewportRectCarac, viewportRectPlus, viewportRectMoins, "Charisme", charisme, chaPlus, chaMoins, y);
        }

        public float afficherChoix(SpriteBatch spriteBatch, SpriteFont font, Rectangle viewportRectCarac, Rectangle viewportRectPlus, Rectangle viewportRectMoins, String carac, int caract, ButtonMenu buttonPlus, ButtonMenu buttonMoins, float y)
        {
            CreatePersoMenuState.afficherTexte(carac + " : " + caract, game, viewportRectCarac, spriteBatch, font, Color.DarkBlue, y);
            buttonPlus.setX(viewportRectPlus.X);
            buttonPlus.setY(y - 10);
            buttonMoins.setX(viewportRectMoins.X);
            buttonMoins.setY(y + 10);
            if (buttonPlus.isNear(5))
            {
                CreatePersoMenuState.afficherTexte("+", game, viewportRectPlus, spriteBatch, font, buttonPlus.getOnClickColor(), y - 10);
            }
            else
            {
                CreatePersoMenuState.afficherTexte("+", game, viewportRectPlus, spriteBatch, font, buttonPlus.getColor(), y - 10);
            }
            if (buttonMoins.isNear(5))
            {
                y = CreatePersoMenuState.afficherTexte("-", game, viewportRectMoins, spriteBatch, font, buttonMoins.getOnClickColor(), y + 10);
            }
            else
            {
                y = CreatePersoMenuState.afficherTexte("-", game, viewportRectMoins, spriteBatch, font, buttonMoins.getColor(), y + 10);
            }
            return y;
        }
    }
}
