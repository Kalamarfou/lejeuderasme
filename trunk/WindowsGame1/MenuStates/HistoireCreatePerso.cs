using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.GameObjects;
using System.Runtime.InteropServices;

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
                button = new ButtonMenu("Suivant", Color.DarkBlue, Color.DarkGreen, new Vector2((game.GraphicsDevice.Viewport.Width) - 150, 9 * game.GraphicsDevice.Viewport.Height / 10 + 20));
                listeButtons.Add(button);

                listeChoix = null;
                descriptions = null;

                choixSelect = null;
                titre = "FINALISATION DE VOTRE PERSONNAGE";
            }

            public override bool conditionValide(ButtonMenu button)
            {
                if (!button.getText().Equals("Suivant") || (histoire != null && prenom != null && nom != null && age != null))
                {
                    return true;
                }
                return false;
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
                /*clavier = Keyboard.GetState();
                bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
                bool NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;

                if ((clavier.GetPressedKeys().Length == 1 || clavier.GetPressedKeys().Length == 2) && (!toucheEnfoncee))
                {
                    toucheEnfoncee = true;
                    touche = clavier.GetPressedKeys()[0].ToString();
                    if (clavier.GetPressedKeys().Length == 2 && touche.Equals("LeftShift"))
                    {
                        shiftActif = true;
                        touche = clavier.GetPressedKeys()[1].ToString();
                    }
                    if (touche.Length == 1 && (!shiftActif) && (!CapsLock))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += touche.ToLower();
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += touche.ToLower();
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += touche.ToLower();
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += touche.ToLower();
                    }
                    else if (touche.Length == 1 && (shiftActif || CapsLock))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += touche.ToUpper();
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += touche.ToUpper();
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += touche.ToUpper();
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += touche.ToUpper();
                        shiftActif = false;
                    }
                    else if (touche.Equals("Space"))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += " ";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += " ";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += " ";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += " ";
                    }
                    else if (touche.Equals("Delete") || touche.Equals("Back"))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && prenom.Length > 0) prenom = prenom.Remove(prenom.Length - 1);
                        else if (estDansLeRectangle(mousePointer, nomRect) && nom.Length > 0) nom = nom.Remove(nom.Length - 1);
                        else if (estDansLeRectangle(mousePointer, ageRect) && age.Length > 0) age = age.Remove(age.Length - 1);
                        else if (estDansLeRectangle(mousePointer, histRect) && histoire.Length > 0) histoire = histoire.Remove(histoire.Length - 1);
                    }
                    else if ((shiftActif || CapsLock) && (touche.Equals("D1") || touche.Equals("D2") || touche.Equals("D3") || touche.Equals("D4") || touche.Equals("D5")
                        ||touche.Equals("D6") ||touche.Equals("D7") ||touche.Equals("D8") ||touche.Equals("D9") ||touche.Equals("D0")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += touche[1];
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += touche[1];
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += touche[1];
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += touche[1];
                        shiftActif = false;
                    }
                    else if (NumLock && (touche.Contains("NumPad")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += touche[6];
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += touche[6];
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += touche[6];
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += touche[6];
                    }
                    else if (NumLock && (touche.Equals("Decimal")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += ".";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += ".";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += ".";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += ".";
                    }
                    else if (!shiftActif && !CapsLock && (touche.Equals("D2")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += "é";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += "é";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += "é";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += "é";
                    }
                    else if (!shiftActif && !CapsLock && (touche.Equals("D4")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += "'";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += "'";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += "'";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += "'";
                    }
                    else if (!shiftActif && !CapsLock && (touche.Equals("D7")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += "è";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += "è";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += "è";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += "è";
                    }
                    else if (!shiftActif && !CapsLock && (touche.Equals("D0")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += "à";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += "à";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += "à";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += "à";
                    }
                    else if (!shiftActif && !CapsLock && (touche.Equals("OemComma")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += ",";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += ",";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += ",";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += ",";
                    }
                    else if ((shiftActif || CapsLock) && (touche.Equals("OemPeriod")))
                    {
                        if (estDansLeRectangle(mousePointer, prenomRect) && (prenom == null || prenom.Length < prenomMax)) prenom += ".";
                        else if (estDansLeRectangle(mousePointer, nomRect) && (nom == null || nom.Length < nomMax)) nom += ".";
                        else if (estDansLeRectangle(mousePointer, ageRect) && (age == null || age.Length < ageMax)) age += ".";
                        else if (estDansLeRectangle(mousePointer, histRect) && (histoire == null || histoire.Length < histoireMax)) histoire += ".";
                        shiftActif = false;
                    }
                    else if (touche.Equals("LeftShift"))
                    {
                        toucheEnfoncee = false;
                        shiftActif = true;
                    }
                }
                else if (clavier.GetPressedKeys().Length == 0) {
                        toucheEnfoncee = false;
                }*/
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

            // An umanaged function that retrieves the states of each key
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
            public static extern short GetKeyState(int keyCode); 
        }
}
