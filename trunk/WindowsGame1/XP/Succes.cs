using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateErasme.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace UltimateErasme.XP
{
    public class Succes : GameObject
    {
        public int NombreDeFoisReussi { get; set; }

        public Timer TimerSucces { get; private set; }
        public string Titre { get; private set; }
        public int XpRecu { get; private set; }
        public SuccesEvents SuccesEvent { get; private set; }
        UltimateErasme game;
        int nombreDeFoisAFarmer;
        

        bool estValide = true;

        public Succes(UltimateErasme game, Texture2D loadedTexture, SuccesEvents succesEvent, int nombreDeFoisAFarmer, string titre, int xpRecu) : base(loadedTexture)
        {
            this.game = game;
            this.nombreDeFoisAFarmer = nombreDeFoisAFarmer;
            this.Titre = titre;
            this.XpRecu = xpRecu;
            this.SuccesEvent = succesEvent;
            NombreDeFoisReussi = 0;

            TimerSucces = new Timer();
        }

        public void TestReussite()
        {
            if (NombreDeFoisReussi > nombreDeFoisAFarmer && estValide)
            {
                TimerSucces.Dispose();
                TimerSucces = new Timer(3500) { Enabled = true };
                TimerSucces.Elapsed += new ElapsedEventHandler(timerSucces_Elapsed);
                XpManager.getInstance(game).AddXpSucces(XpRecu);
                Terminer();
            }
        }

        void timerSucces_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerSucces.Enabled = false;
        }

        private void Terminer()
        {
            estValide = false;
        }
    }
}
