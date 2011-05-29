using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.GameObjects;

namespace UltimateErasme.MenuStates
{
        class HistoireCreatePerso : DescriptionTypes
        {
            Game game;
            private string histoire;
            private string nom;
            private string age;
            private string prenom;

            string touche = null;
            KeyboardState clavier;
            bool toucheEnfoncee = false;

            Rectangle prenomRect = new Rectangle(300, 100, 200, 50);
            Rectangle nomRect = new Rectangle(300, 140, 200, 50);
            Rectangle ageRect = new Rectangle(300, 180, 200, 50);
            Rectangle histRect = new Rectangle(300, 220, 300, 300);

            public HistoireCreatePerso(Game game)
            {
                this.game = game;
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

                choixSelect = null;
                titre = "FINALISATION DE VOTRE PERSONNAGE";
            }

            public override String getValeurRecommande(PersoFinal persoFinal)
            {
                histoire = persoFinal.histoire;
                nom = persoFinal.nom;
                prenom = persoFinal.prenom;
                age = persoFinal.age;

                return null;
            }

            public override void setValeurRecommande(PersoFinal persoFinal, String value)
            {
                persoFinal.histoire = histoire;
                persoFinal.nom = nom;
                persoFinal.prenom = prenom;
                persoFinal.age = age;
            }

            public override void gestionClavier(GameObject mousePointer)
            {
                clavier = Keyboard.GetState();
                if ((clavier.GetPressedKeys().Length == 1) && (!toucheEnfoncee))
                {
                    toucheEnfoncee = true;
                    touche = clavier.GetPressedKeys()[0].ToString();

                    if (touche.Length == 1)
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect)) prenom += touche;
                        else if (estDansLeRectangle(mousePointer, nomRect)) nom += touche;
                        else if (estDansLeRectangle(mousePointer, ageRect)) age += touche;
                        else if (estDansLeRectangle(mousePointer, histRect)) histoire += touche;
                    }
                }
                else
                {
                    if (clavier.GetPressedKeys().Length == 0)
                        toucheEnfoncee = false;
                }
            }

            public override void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch, Game game, Dictionary<String, List<Descriptions>> descriptions, SpriteFont font)
            {
                CreatePersoMenuState.afficherTexte("Prénom : " + prenom, game, prenomRect, spriteBatch, font, Color.DarkBlue, prenomRect.Y);
                CreatePersoMenuState.afficherTexte("Nom : " + nom, game, nomRect, spriteBatch, font, Color.DarkBlue, nomRect.Y);
                CreatePersoMenuState.afficherTexte("Age : " + age, game, ageRect, spriteBatch, font, Color.DarkBlue, ageRect.Y);
                CreatePersoMenuState.afficherTexte("Histoire : " + histoire, game, histRect, spriteBatch, font, Color.DarkBlue, histRect.Y);
            }

            public override void DrawChoix(SpriteBatch spriteBatch, List<ButtonMenu> listeChoix, Rectangle viewportRect, Game game, String choixSelect, SpriteFont font)
            {
            }

            private bool estDansLeRectangle(GameObject mousePointer, Rectangle viewportRect)
            {
                return (mousePointer.Position.X >= viewportRect.X && mousePointer.Position.X <= (viewportRect.X + viewportRect.Width)
                    && mousePointer.Position.Y >= viewportRect.Y && mousePointer.Position.Y <= (viewportRect.Y + viewportRect.Height));
            }
        }
}
