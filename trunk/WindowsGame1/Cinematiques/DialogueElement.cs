using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using UltimateErasme.InputTesters;
using Microsoft.Xna.Framework.Input;
using UltimateErasme.Sound;

namespace UltimateErasme.Cinematiques
{
    public class DialogueElement
    {

        public ArrayList Personnages { get; private set; }
        public List<string> Text { get; private set; }
		public string Sound { get; private set; }
        public Color Color { get; private set; }
        public bool Played { get; set; }
        public bool SoundPlayed { get; set; }
        public SpriteFont Font { get; private set; }
        public List<Reponse> reponses { get; set; }
        public List<string> TextParts { get; set; }
        public int TimingPersonnages { get; set; }
        public int TimingDefilement { get; set; }

        private int currentPersonnage;
        private DateTime timeOfLastPersonnageChange;
        private DateTime timeOfLastTextChange;
        private string fullText;

        private SoundManager soundManager;

#if !XBOX
        KeyboardTester keyboardTester = new KeyboardTester();
#endif

        public DialogueElement(ArrayList personnages, int timingPersonnages, string text, int timingDefilement, string sound, Color color, SpriteFont font, SoundManager soundManager)
        {
            this.Personnages = personnages;
            currentPersonnage = 0;
            
            this.Color = color;
            this.Font = font;
			this.Sound = sound;
            this.SoundPlayed = false;
            this.TimingPersonnages = timingPersonnages;
            this.TimingDefilement = timingDefilement;

            if (TimingDefilement > 0)
            {
                this.fullText = text;
                this.Text = "".Split(' ').ToList<string>();
            }
            else
            {
                this.Text = text.Split(' ').ToList<string>();
            }

            timeOfLastPersonnageChange = DateTime.Now;
            timeOfLastTextChange = DateTime.Now;

            reponses = new List<Reponse>();
            TextParts = new List<string>();

            string tmp = "";
            foreach (string item in Text)
            {
                tmp = tmp + " " + item;
                if (tmp.Length > 45)
                {
                    TextParts.Add(tmp);
                    tmp = "";
                }
            }
            TextParts.Add(tmp);

            this.soundManager = soundManager;

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
			try{
                int h = ((GameObject)Personnages[currentPersonnage]).Sprite.Height;
                spriteBatch.Draw(((GameObject)Personnages[currentPersonnage]).Sprite, new Vector2(0, 600 - h), Color.White);
			}
			catch (Exception e) {
                Console.WriteLine(e);
            }

            int sautDeLigne = 0;
            foreach (String item in TextParts)
            {
                spriteBatch.DrawString(Font, item, new Vector2(220, 400 + sautDeLigne), this.Color);
                sautDeLigne = sautDeLigne + 20;
            }

            if (reponses.Count > 0)
            {
                int i = 30;
                foreach (Reponse rep in reponses)
                {
                    rep.Font = Font;
                    rep.reponsePosition = new Vector2(300, 400 + sautDeLigne + i);
                    i = i + 30;
                    rep.Draw(spriteBatch, gameTime);
                }
            }
            
        }


        internal void Update(GameTime gameTime)
        {
            
            if (Personnages.Count > 1)
            {
                if (fullText.Length > 0)
                {
                    SwapPersonnage();
                }                
            }
            if (TimingDefilement > 0)
            {
                TestDefilerTexte();
            }
#if !XBOX

            keyboardTester.GetKeyboard();
            //vrai update des boutons
            UpdateKeyboard(gameTime);
            keyboardTester.UpdatePreviousKeyboardState();

#endif
        }

        private void UpdateKeyboard(GameTime gameTime)
        {
            if (keyboardTester.test(Keys.Tab))
            {
                TimingDefilement = 1;
            }
        }

        private void TestDefilerTexte()
        {
            TimeSpan diffResult = DateTime.Now.Subtract(timeOfLastTextChange);
            if (diffResult.TotalMilliseconds > TimingDefilement)
            {
                if (fullText.Length > 0)
                {
                    DefilerTexte();
                }
            }
        }

        private void DefilerTexte()
        {
            int count = Text.Count<string>();
            if (fullText[0] == ' ')
            {
                fullText = fullText.Remove(0, 1);
                Text.Add(fullText[0].ToString());
                fullText = fullText.Remove(0, 1);
            }
            else
            {
                Text[count - 1] = Text[count - 1] + fullText[0].ToString();
                fullText = fullText.Remove(0, 1);
            }

            RegenerateTextParts();

            if (TimingDefilement > 15)
            {
                try
                {
                    
                soundManager.Play("Tut");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            timeOfLastTextChange = DateTime.Now;
        }

        private void RegenerateTextParts()
        {
            TextParts = new List<string>();

            string tmp = "";
            foreach (string item in Text)
            {
                tmp = tmp + " " + item;
                if (tmp.Length > 45)
                {
                    TextParts.Add(tmp);
                    tmp = "";
                }
            }
            TextParts.Add(tmp);
        }

        private void SwapPersonnage()
        {
            TimeSpan diffResult = DateTime.Now.Subtract(timeOfLastPersonnageChange);
            if (diffResult.TotalMilliseconds > TimingPersonnages)
	        {
                if (currentPersonnage < Personnages.Count - 1)
                {
                    currentPersonnage++;
                }
                else
                {
                    currentPersonnage = 0;
                }
                timeOfLastPersonnageChange = DateTime.Now;
	        }
        }
    }
}
