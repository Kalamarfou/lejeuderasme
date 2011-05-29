using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;

namespace UltimateErasme.MenuStates
{
    abstract class DescriptionTypes
    {
        public abstract void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre);
        public abstract String getValeurRecommande(PersoFinal persoFinal);
        public abstract void setValeurRecommande(PersoFinal persoFinal, String value);
        public virtual void changeCaracValue() { }
        public virtual void gestionClavier(GameObject mousePointer) { }

        public void DrawButtons(SpriteBatch spriteBatch, List<ButtonMenu> listeButtons, SpriteFont font)
        {
            foreach (ButtonMenu button in listeButtons)
            {
                if (button.isNear())
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getOnClickColor());
                }
                else
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), button.getColor());
                }
            }
        }

        public void DrawTitle(SpriteBatch spriteBatch, Game game, String titre, SpriteFont font)
        {
            spriteBatch.DrawString(font, titre, new Vector2(game.GraphicsDevice.Viewport.Height / 2, game.GraphicsDevice.Viewport.Height / 20), Color.DarkRed);
        }

        public virtual void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch, Game game, Dictionary<String, List<Descriptions>> descriptions, SpriteFont font)
        {
            List<Descriptions> descriptionChoice;
            descriptions.TryGetValue(choix, out descriptionChoice);

            //spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
            float y = game.GraphicsDevice.Viewport.Height / 10;
            foreach (Descriptions description in descriptionChoice)
            {
                CreatePersoMenuState.afficherTexte(description.titre, game, viewportRect, spriteBatch, font, Color.DarkBlue, y);
                y = CreatePersoMenuState.afficherTexte(description.description, game, viewportRect, spriteBatch, font, Color.DarkBlue, y + 20);
                y += 40;
            }
        }

        public virtual void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
        {
            if (listeChoix != null)
            {
                float y = viewportRect.Y;
                foreach (ButtonMenu button in listeChoix)
                {
                    String texte = button.getText();
                    
                    if (button.isNear() || button.getText().Equals(choixSelect))
                    {
                        y = CreatePersoMenuState.afficherTexte(texte, game, viewportRect, spriteBatch, font, button.getOnClickColor(), y);
                    }
                    else
                    {
                        y = CreatePersoMenuState.afficherTexte(texte, game, viewportRect, spriteBatch, font, button.getColor(), y);
                    }
                    y += 40;
                }
            }
        }

        public void DrawPersonnage(Rectangle viewportRect, SpriteBatch spriteBatch, GameObject personnage)
        {
            spriteBatch.Draw(personnage.Sprite, viewportRect, Color.White);
        }
    }
}
