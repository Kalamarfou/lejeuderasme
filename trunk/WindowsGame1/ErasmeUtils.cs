using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace UltimateErasme
{
    class ErasmeUtils
    {
        static KeyboardState clavier;
        static bool shiftActif = false;

        public static float afficherTexte(String texte, Game game, Rectangle viewportRect, SpriteBatch spriteBatch, SpriteFont font, Color color, float debut)
        {
            String[] mots = texte.Split(' ');
            String motToPrint;
            float y = debut;
            int tailleRestanteLigne = (viewportRect.Width / 11);
            int idDebut = 0, tailleRestanteLigneFinale = 0;

            foreach (String mot in mots)
            {
                if (mot.Equals(mots.Last()))
                    motToPrint = mot;
                else
                    motToPrint = mot + " ";
                int tailleMot = motToPrint.Length;

                if (tailleMot <= tailleRestanteLigne)
                {
                    spriteBatch.DrawString(font, motToPrint, new Vector2(viewportRect.X + (viewportRect.Width - 11 * tailleRestanteLigne), y), color);
                    tailleRestanteLigne -= tailleMot;
                    idDebut += tailleMot;
                }
                else if (tailleMot <= viewportRect.Width / 11)
                {
                    y += 20;
                    spriteBatch.DrawString(font, motToPrint, new Vector2(viewportRect.X, y), color);
                    idDebut += tailleMot;
                    tailleRestanteLigne = viewportRect.Width / 11 - tailleMot;
                }
                else
                {
                    //Mot plus grand qu'une ligne
                    y = afficherCaractParCaract(motToPrint, game, viewportRect.X, viewportRect.X + (viewportRect.Width - 11 * tailleRestanteLigne), y, tailleRestanteLigne, viewportRect.Width / 11, spriteBatch, font, color, out tailleRestanteLigneFinale);
                    tailleRestanteLigne = tailleRestanteLigneFinale;
                    idDebut += tailleMot;
                }
            }
            return y;
        }

        private static float afficherCaractParCaract(String mot, Game game, float xInit, float x, float y, int tailleRestanteLigne, int tailleMax, SpriteBatch spriteBatch, SpriteFont font, Color color, out int tailleRestanteLigneFinale)
        {
            int tailleRestanteAEcrire = mot.Length;
            int i = 0;
            while (i < mot.Length)
            {
                if (tailleRestanteAEcrire >= tailleRestanteLigne)
                {
                    spriteBatch.DrawString(font, mot.Substring(i, tailleRestanteLigne), new Vector2(x, y), color);
                    tailleRestanteAEcrire -= tailleRestanteLigne;
                    i += tailleRestanteLigne;
                    y += 20;
                    tailleRestanteLigne = tailleMax;
                    x = xInit;
                }
                else
                {
                    spriteBatch.DrawString(font, mot.Substring(i, tailleRestanteAEcrire), new Vector2(x, y), color);
                    i += tailleRestanteAEcrire;
                    tailleRestanteAEcrire = 0;
                }
            }
            tailleRestanteLigneFinale = tailleRestanteLigne;
            return y;
        }

        //TODO : Gérer la manette lol lol lol dur !
        public static String gestionClavier(GraphicsDeviceManager graphics, GameObject mousePointer, Rectangle viewportRect, String champValue, int champValueMax, bool toucheEnfoncee, out bool outToucheEnfoncee)
        {
            string touche = null;

            clavier = Keyboard.GetState();
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
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += touche.ToLower();
                }
                else if (touche.Length == 1 && (shiftActif || CapsLock))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += touche.ToUpper();
                }
                else if (touche.Equals("Space"))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += " ";
                }
                else if (touche.Equals("Delete") || touche.Equals("Back"))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && champValue.Length > 0) champValue = champValue.Remove(champValue.Length - 1);
                }
                else if ((shiftActif || CapsLock) && (touche.Equals("D1") || touche.Equals("D2") || touche.Equals("D3") || touche.Equals("D4") || touche.Equals("D5")
                    || touche.Equals("D6") || touche.Equals("D7") || touche.Equals("D8") || touche.Equals("D9") || touche.Equals("D0")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += touche[1];
                    shiftActif = false;
                }
                else if (NumLock && (touche.Contains("NumPad")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += touche[6];
                }
                else if (NumLock && (touche.Equals("Decimal")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += ".";
                }
                else if (!shiftActif && !CapsLock && (touche.Equals("D2")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += "é";
                }
                else if (!shiftActif && !CapsLock && (touche.Equals("D4")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += "'";
                }
                else if (!shiftActif && !CapsLock && (touche.Equals("D7")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += "è";
                }
                else if (!shiftActif && !CapsLock && (touche.Equals("D0")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += "à";
                }
                else if (!shiftActif && !CapsLock && (touche.Equals("OemComma")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += ",";
                }
                else if ((shiftActif || CapsLock) && (touche.Equals("OemPeriod")))
                {
                    if (estDansLeRectangle(mousePointer, viewportRect) && (champValue == null || champValue.Length < champValueMax)) champValue += ".";
                    shiftActif = false;
                }
                else if (touche.Equals("LeftShift"))
                {
                    toucheEnfoncee = false;
                    shiftActif = true;
                }
            }
            else if (clavier.GetPressedKeys().Length == 0)
            {
                toucheEnfoncee = false;
            }
            outToucheEnfoncee = toucheEnfoncee;
            return champValue;
        }

        private static bool estDansLeRectangle(GameObject mousePointer, Rectangle viewportRect)
        {
            return (mousePointer.Position.X >= viewportRect.X && mousePointer.Position.X <= (viewportRect.X + viewportRect.Width)
                && mousePointer.Position.Y >= viewportRect.Y && mousePointer.Position.Y <= (viewportRect.Y + viewportRect.Height));
        }

        // An umanaged function that retrieves the states of each key
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode); 
    }
}
