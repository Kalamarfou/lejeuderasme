using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UltimateErasme.Cinematiques
{
    public static class CinematiquesManager_old
    {
        private static ContentManager contentManager;
        private static SpriteBatch spriteBatch;
        private static SpriteFont dialogueFont;

        public static void init(ContentManager content, SpriteBatch sb)
        {
            contentManager = content;
            spriteBatch = sb;
            dialogueFont = contentManager.Load<SpriteFont>("Fonts/DialogueFont");
        }

        public static void playCinematic(string chemin)
        {
            XElement rootElement = XDocument.Load(chemin).Root;
            foreach (XElement element in rootElement.Elements())
            {
                if (element.Name == "DialogueElement")
                {
                    ManageDialogueElement(element);
                }
            }
        }

        private static void ManageDialogueElement(XElement element)
        {
            GameObject personnage;
            string text;
            Color color;

            if (element.Attribute("personnage") != null)
            {
                personnage = new GameObject(contentManager.Load<Texture2D>(element.Attribute("personnage").Value));
            }
            else
            {
                personnage = new GameObject(contentManager.Load<Texture2D>(@"Sprites\Dialogues\Empty"));
            }

            if (element.Attribute("texte") != null)
            {
                text = element.Attribute("texte").Value;
            }
            else
            {
                text = "";
            }

            if (element.Attribute("color") != null)
            {
                color = Color.Black;
            }
            else
            {
                color = Color.Black;
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(dialogueFont, text, new Vector2(20, 170), color);
            spriteBatch.End();

        }
    }
}
