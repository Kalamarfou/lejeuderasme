using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

            bool toucheEnfonceePrenom = false;
            bool toucheEnfonceeNom = false;
            bool toucheEnfonceeAge = false;
            bool toucheEnfonceeHist = false;

            int prenomMax = 20;
            int nomMax = 30;
            int histoireMax = 150;
            int ageMax = 3;
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
                button = new ButtonMenu("Terminer", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
                listeButtons.Add(button);

                listeChoix = null;
                descriptions = null;

                choixSelect = null;
                titre = "FINALISATION DE VOTRE PERSONNAGE";
            }

            public override bool conditionValide(ButtonMenu button)
            {
                if (button.getText().Equals("Terminer") && (histoire == null || prenom == null || nom == null || age == null || prenom.Contains(".")
                    || nom.Contains("_") || prenom.Contains("_") || nom.Contains(".") || SavedPersoMenuState.listePerso.ContainsKey(prenom + "_" + nom)))
                {
                    return false;
                }
                return true;
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
                if (histoire != null)
                    persoFinal.histoire = histoire;
                if (nom != null)
                    persoFinal.nom = nom;
                if (prenom != null)
                    persoFinal.prenom = prenom;
                short mov;
                if (Int16.TryParse(age, out mov)) {
                    persoFinal.age = age;
                }

            }
            //TODO : Gérer la manette lol lol lol dur !
            public override void gestionClavier(GraphicsDeviceManager graphics, GameObject mousePointer)
            {
                prenom = ErasmeUtils.gestionClavier(graphics, mousePointer, prenomRect, prenom, prenomMax, toucheEnfonceePrenom, out toucheEnfonceePrenom);
                nom = ErasmeUtils.gestionClavier(graphics, mousePointer, nomRect, nom, nomMax, toucheEnfonceeNom, out toucheEnfonceeNom);
                age = ErasmeUtils.gestionClavier(graphics, mousePointer, ageRect, age, ageMax, toucheEnfonceeAge, out toucheEnfonceeAge);
                histoire = ErasmeUtils.gestionClavier(graphics, mousePointer, histRect, histoire, histoireMax, toucheEnfonceeHist, out toucheEnfonceeHist);
            }

            public override void DrawDescription(String choix, Rectangle viewportRect, SpriteBatch spriteBatch, Game game, Dictionary<String, List<Descriptions>> descriptions, SpriteFont font)
            {
                ErasmeUtils.afficherTexte("Prénom : " + prenom, game, prenomRect, spriteBatch, font, Color.DarkBlue, prenomRect.Y);
                ErasmeUtils.afficherTexte("Nom : " + nom, game, nomRect, spriteBatch, font, Color.DarkBlue, nomRect.Y);
                ErasmeUtils.afficherTexte("Age : " + age, game, ageRect, spriteBatch, font, Color.DarkBlue, ageRect.Y);
                ErasmeUtils.afficherTexte("Histoire : " + histoire, game, histRect, spriteBatch, font, Color.DarkBlue, histRect.Y);
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
