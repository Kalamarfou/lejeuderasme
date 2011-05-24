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
            int tailleRestante = 0, i = 0;
            foreach (Descriptions description in descriptionChoice)
            {
                tailleRestante = description.description.Length;
                i = 0;
                spriteBatch.DrawString(font, description.titre, new Vector2(viewportRect.X, y), Color.DarkBlue);
                while (i < description.description.Length)
                {
                    int tailleMax = (viewportRect.Width / 11);
                    if (tailleRestante > tailleMax)
                    {
                        spriteBatch.DrawString(font, description.description.Substring(i, tailleMax), new Vector2(viewportRect.X, y + 20), Color.DarkBlue);
                        tailleRestante -= tailleMax;
                        i += tailleMax;
                    }
                    else
                    {
                        spriteBatch.DrawString(font, description.description.Substring(i, tailleRestante), new Vector2(viewportRect.X, y + 20), Color.DarkBlue);
                        i += tailleRestante;
                        tailleRestante = 0;
                    }
                    y += 20;
                }
                y += 40;
            }
        }

        public virtual void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
        {
            if (listeChoix != null)
            {
                float y = 0;
                foreach (ButtonMenu button in listeChoix)
                {
                    String texte = button.getText();
                    int tailleRestante = texte.Length;
                    int i = 0;

                    if (button.isNear() || button.getText().Equals(choixSelect))
                    {
                        while (i < texte.Length)
                        {
                            int tailleMax = (viewportRect.Width / 11);
                            if (tailleRestante > tailleMax)
                            {
                                spriteBatch.DrawString(font, texte.Substring(i, tailleMax), new Vector2(button.getX(), y + button.getY()), button.getOnClickColor());
                                tailleRestante -= tailleMax;
                                i += tailleMax;
                            }
                            else
                            {
                                spriteBatch.DrawString(font, texte.Substring(i, tailleRestante), new Vector2(button.getX(), y + button.getY()), button.getOnClickColor());
                                i += tailleRestante;
                                tailleRestante = 0;
                            }
                            y += 20;
                        }
                    }
                    else
                    {
                        while (i < texte.Length)
                        {
                            int tailleMax = (viewportRect.Width / 11);
                            if (tailleRestante > tailleMax)
                            {
                                spriteBatch.DrawString(font, texte.Substring(i, tailleMax), new Vector2(button.getX(), y + button.getY()), button.getColor());
                                tailleRestante -= tailleMax;
                                i += tailleMax;
                            }
                            else
                            {
                                spriteBatch.DrawString(font, texte.Substring(i, tailleRestante), new Vector2(button.getX(), y + button.getY()), button.getColor());
                                i += tailleRestante;
                                tailleRestante = 0;
                            }
                            y += 20;
                        }
                    }
                    y -= 20;
                }
            }
        }

        public void DrawPersonnage(Rectangle viewportRect, SpriteBatch spriteBatch, GameObject personnage)
        {
            spriteBatch.Draw(personnage.Sprite, viewportRect, Color.White);
        }
    }
}
