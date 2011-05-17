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
using UltimateErasme.Sound;
using Microsoft.Xna.Framework.Design;


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

		private SoundManager soundManager;
		
        public delegate void SetPause(bool value);

        public SetPause setPause;
        
        public Texture2D dialogueBackground;


        GamePadTester gamePadTester = new GamePadTester();
#if !XBOX
        KeyboardTester keyboardTester = new KeyboardTester();
#endif


        public CinematiquesManager(UltimateErasme game)
            : base(game.game)
        {
            contentManager = game.Content;
            spriteBatch = new SpriteBatch(game.game.GraphicsDevice);
            setPause = new SetPause(game.SetPause);
            soundManager = game.playerManager.premierJoueur.soundManager;
            dialogueBackground = game.Content.Load<Texture2D>(@"Sprites\Dialogues\Background");
            this.Initialize();
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
            if (!cinematiquePlaying)
            {
                currentCinematic.Clear();
                XElement rootElement = XDocument.Load(chemin).Root;
                foreach (XElement element in rootElement.Elements())
                {
                    if (element.Name == "DialogueElement")
                    {
                        ManageDialogueElement(currentCinematic, element);
                    }
                }
                currentElement = currentCinematic[0];
                setPause(true);
                cinematiquePlaying = true;

            }
        }

        private void ManageDialogueElement(List<DialogueElement> brancheDialogue,XElement element)
        {
            ArrayList personnages = new ArrayList();
            string text;
			string sound;
            Color color;
            DialogueElement dialogueElement;
            int timingPersonnage;
            int timingDefilement;

            if (element.Attribute("personnage") != null)
            {
                
				try{
                    string[] temp = element.Attribute("personnage").Value.Split(';');
                    foreach (string item in temp)
                    {   
                        GameObject personnage = new GameObject(contentManager.Load<Texture2D>(item));
                        personnages.Add(personnage);
                    }
				}
				catch (Exception e) {
                    GameObject personnage = new GameObject(contentManager.Load<Texture2D>(@"Sprites\Dialogues\Empty"));
                    personnages.Add(personnage);
                }
            }
            else
            {
                GameObject personnage = new GameObject(contentManager.Load<Texture2D>(@"Sprites\Dialogues\Empty"));
                personnages.Add(personnage);
            }

            if (element.Attribute("timingImages") != null)
            {
                try
                {
                    timingPersonnage = int.Parse(element.Attribute("timingImages").Value);
                }
                catch (Exception e)
                {
                    timingPersonnage = 600;
                }
                
            }
            else
            {
                timingPersonnage = 600;
            }


            if (element.Attribute("texte") != null)
            {
                text = element.Attribute("texte").Value;
            }
            else
            {
                text = "";
            }

            if (element.Attribute("timingDefilement") != null)
            {
                try
                {
                    timingDefilement = int.Parse(element.Attribute("timingDefilement").Value);
                }
                catch (Exception e)
                {
                    timingDefilement = 70;
                }

            }
            else
            {
                timingDefilement = 70;
            }


            if (element.Attribute("color") != null)
            {
                try
                {
                    System.Drawing.Color c = System.Drawing.Color.FromName(element.Attribute("color").Value);
                    color = new Color(c.R, c.G, c.B, c.A);
                }
                catch (Exception)
                {
                    color = Color.Black;
                }
                
            }
            else
            {
                color = Color.Black;
            }
			
			if (element.Attribute("son") != null)
            {
                sound = element.Attribute("son").Value;
            }
            else
            {
                sound =  "";
            }

            // on crée l'élément
            dialogueElement = new DialogueElement(personnages, timingPersonnage, text, timingDefilement, sound, color, dialogueFont,soundManager );

            //a debordeliser
            if (element.HasElements)
            {
                foreach (XElement reponse in element.Elements())
                {
                    if (reponse.Name == "Reponse")
                    {
                        Reponse rep = new Reponse(reponse.Attribute("texte").Value, contentManager);
                        
                        if (reponse.HasElements)
                        {
                            foreach (var item in reponse.Elements())
                            {
                                ManageDialogueElement(rep.includedElements, item);
                            }
                        }

                        dialogueElement.reponses.Add(rep);
                    }
                }
            }

            if (dialogueElement.reponses.Count > 0)
            {
                dialogueElement.reponses.First<Reponse>().Selected = true;
            }
            brancheDialogue.Add(dialogueElement);

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
                currentElement.Update(gameTime);
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
            else if (keyboardTester.test(Keys.Up))
            {
                UpdateSelectedResponse(Keys.Up);
            }
            else if (keyboardTester.test(Keys.Down))
            {
                UpdateSelectedResponse(Keys.Down);
            }
        }

        private void UpdateSelectedResponse(Keys keys)
        {
            int i = GetResponseToSelect(keys);
            if (i >= 0 && i < currentElement.reponses.Count)
            {
                foreach (Reponse rep in currentElement.reponses)
                {
                    if (rep.Selected)
                    {
                        rep.Selected = false;
                    }
                }
                currentElement.reponses[i].Selected = true;
            }
        }

        private int GetResponseToSelect(Keys keys)
        {
            int i = 0;
            foreach (Reponse rep in currentElement.reponses)
            {
                if (rep.Selected)
                {
                    if (keys == Keys.Down)
                    {
                        i++;
                    }
                    if (keys == Keys.Up)
                    {
                        i--;
                    }
                    return i;
                }
                i++;
            }
            return -1;
        }

        private void NextElement()
        {
            //aaaaah, ça deviens incomprehensible 
            currentElement.Played = true;
            foreach (DialogueElement element in currentCinematic)
            {
                if (currentElement.reponses.Count > 0)
                {
                    foreach (Reponse rep in currentElement.reponses)
                    {
                        if (rep.Selected)
                        {
                            currentElement = rep.includedElements.First();
                            return;
                        }
                    }
                }
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
                spriteBatch.Begin();
                spriteBatch.Draw(dialogueBackground, new Vector2(160, 600 - dialogueBackground.Height), Color.White);
                currentElement.Draw(spriteBatch, gameTime);
                if (!currentElement.SoundPlayed)
                {
				    soundManager.Play(currentElement.Sound);
                    currentElement.SoundPlayed = true;
                }
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}