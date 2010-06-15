using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Linq;
using UltimateErasme.GameObjects;
using System.Collections;
using UltimateErasme.GameObjects.enums;
using UltimateErasme.InputTesters;


namespace UltimateErasme.Cinematiques
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CinematiquesManager : DrawableGameComponent
    {

        private SpriteFont dialogueFont;
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;

        private bool cinematiquePlaying = false;
        private List<DialogueElement> currentCinematic = new List<DialogueElement>();
        private DialogueElement currentElement;

        public delegate void SetPause(bool value);

        public SetPause setPause;

        GamePadTester gamePadTester = new GamePadTester();
#if !XBOX
        KeyboardTester keyboardTester = new KeyboardTester();
#endif


        public CinematiquesManager(UltimateErasme game)
            : base(game)
        {
            contentManager = game.Content;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            setPause = new SetPause(game.SetPause);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            dialogueFont = contentManager.Load<SpriteFont>("Fonts/DialogueFont");
            base.Initialize();
        }

        public void playCinematic(string chemin)
        {
            currentCinematic.Clear();
            XElement rootElement = XDocument.Load(chemin).Root;
            foreach (XElement element in rootElement.Elements())
            {
                if (element.Name == "DialogueElement")
                {
                    ManageDialogueElement(element);
                }
            }
            currentElement = currentCinematic[0];
            setPause(true);
            cinematiquePlaying = true;
        }

        private void ManageDialogueElement(XElement element)
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

            currentCinematic.Add(new DialogueElement(personnage, text, color));

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (cinematiquePlaying)
            {
                gamePadTester.ChooseGamePad(ControllerType.xBoxControler1);
                //vrai update des boutons
                UpdateXboxController(gameTime);
                gamePadTester.UpdatePreviousGamePadState();


#if !XBOX

                keyboardTester.GetKeyboard();
                //vrai update des boutons
                UpdateKeyboard(gameTime);
                keyboardTester.UpdatePreviousKeyboardState();

#endif
            }

            base.Update(gameTime);
        }

        private void UpdateXboxController(GameTime gameTime)
        {
            if (gamePadTester.test(Buttons.A))
            {
                NextElement();
            }
        }


        private void UpdateKeyboard(GameTime gameTime)
        {
            if (keyboardTester.test(Keys.Space))
            {
                NextElement();
            }
        }

        private void NextElement()
        {
            currentElement.Played = true;
            foreach (DialogueElement element in currentCinematic)
            {
                if (!element.Played)
                {
                    currentElement = element;
                    return;
                }
            }
            cinematiquePlaying = false;
            setPause(false);
            currentCinematic.Clear();
        }

        public override void Draw(GameTime gameTime)
        {
            if (cinematiquePlaying)
            {
                currentElement.Draw(spriteBatch, gameTime);
            }
            base.Draw(gameTime);
        }
    }
}