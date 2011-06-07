using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using UltimateErasme.InputTesters;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme.MenuStates
{
    abstract class DescriptionTypes
    {

        KeyboardTester keyboardTester = new KeyboardTester();

        public abstract void remplissageDonneesCreationPerso(out List<ButtonMenu> listeButtons, out List<ButtonMenu> listeChoix, out Dictionary<String, List<Descriptions>> descriptions, out String choixSelect, out String titre);
        public abstract String getValeurRecommande(PersoFinal persoFinal);
        public abstract void setValeurRecommande(PersoFinal persoFinal, String value);
        public virtual void changeCaracValue() { }
        public virtual void gestionClavier(GraphicsDeviceManager graphics, GameObject mousePointer) {
            keyboardTester.GetKeyboard();

            if (keyboardTester.test(Keys.F))
                graphics.ToggleFullScreen();
        }
        public virtual bool conditionValide(ButtonMenu button) { return true; }

        public void DrawButtons(SpriteBatch spriteBatch, List<ButtonMenu> listeButtons, SpriteFont font)
        {
            foreach (ButtonMenu button in listeButtons)
            {
                if (conditionValide(button))
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
                else
                {
                    spriteBatch.DrawString(font, button.getText(), new Vector2(button.getX(), button.getY()), Color.DarkGray);
                }
            }
        }

        public void DrawTitle(SpriteBatch spriteBatch, Game game, String titre, SpriteFont font)
        {
            spriteBatch.DrawString(font, titre, new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 20), Color.DarkRed);
        }

        public virtual void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch, Game game, Dictionary<String, List<Descriptions>> descriptions, SpriteFont font)
        {
            List<Descriptions> descriptionChoice;
            descriptions.TryGetValue(choix, out descriptionChoice);

            //spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
            float y = game.GraphicsDevice.Viewport.Height / 10;
            foreach (Descriptions description in descriptionChoice)
            {
                ErasmeUtils.afficherTexte(description.titre, game, viewportRect, spriteBatch, font, Color.DarkBlue, y);
                y = ErasmeUtils.afficherTexte(description.description, game, viewportRect, spriteBatch, font, Color.DarkBlue, y + 20);
                y += 40;
            }
        }

        public virtual void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
        {
            Rectangle rec;
            if (listeChoix != null)
            {
                foreach (ButtonMenu button in listeChoix)
                {
                    String texte = button.getText();
                    rec = new Rectangle((int)button.getX(), (int)button.getY(), viewportRect.Width, viewportRect.Height / 6); 
                    if (button.isNear() || button.getText().Equals(choixSelect))
                    {
                        ErasmeUtils.afficherTexte(texte, game, rec, spriteBatch, font, button.getOnClickColor(), rec.Y);
                    }
                    else
                    {
                        ErasmeUtils.afficherTexte(texte, game, rec, spriteBatch, font, button.getColor(), rec.Y);
                    }
                }
            }
        }

        public void DrawPersonnage(Rectangle viewportRect, SpriteBatch spriteBatch, GameObject personnage)
        {
            spriteBatch.Draw(personnage.Sprite, viewportRect, Color.White);
        }
    }
}
