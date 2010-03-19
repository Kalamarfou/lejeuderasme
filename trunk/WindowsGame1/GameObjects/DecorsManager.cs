using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections;
using UltimateErasme.ClassesDInternet.Particles;
using UltimateErasme.Sound;
using UltimateErasme.GameObjects.enums;

namespace UltimateErasme.GameObjects
{
    public class DecorsManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;
        public int level = 1;

        public GameObject background;

        public DecorsManager(UltimateErasme game, Rectangle viewportRect)
        {
            this.viewportRect = viewportRect;
            viewportRectPlus = new Rectangle(viewportRect.X, viewportRect.Y, viewportRect.Width + 100, viewportRect.Height + 100);
            this.game = game;
            background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor"));

        }


        public void Update(GameTime gameTime)
        {
            TestPositionsPersos();
        }

        private void TestPositionsPersos()
        {
            if (game.playerManager.nombreDeJoueurs == NombreDeJoueurs.solo)
            {
                TestPositionsPersosSolo();
            }
            else
            {
                TestPositionsPersosDeuxJoueurs();
            }
        }

        private void TestPositionsPersosDeuxJoueurs()
        {
            Point p1 = game.playerManager.premierJoueur.getVulnerableBox().Center;
            Point p2 = game.playerManager.deuxiemeJoueur.getVulnerableBox().Center;
            if (p1.X > viewportRect.Width && p2.X > viewportRect.Width)
            {
                NiveauSuivant();
            }
            else if (p1.X < 0 && p2.X < 0)
            {
                NiveauPrecedent();
            }
        }

        private void TestPositionsPersosSolo()
        {
            Point p = game.playerManager.premierJoueur.getVulnerableBox().Center;
            if (p.X > viewportRect.Width)
            {
                NiveauSuivant();
            }
            else if (p.X < 0)
            {
                NiveauPrecedent();
            }
        }

        private void NiveauPrecedent()
        {
            level--;
            if (level < 1)
            {
                RemettreErasmeAuDebut();
                level = 1;
                return;
            }
            else if (level == 1)
            {
                background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor"));
                RemettreErasmeALaFin();
            }
            else if (level <= 5)
            {
                background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor" + level));
                RemettreErasmeALaFin();
            }
        }

        

        private void NiveauSuivant()
        {
            level++;
            if (level <= 5)
            {
                background = new GameObject(game.Content.Load<Texture2D>(@"Sprites\Backgrounds\decor" + level));
                RemettreErasmeAuDebut();
            }
        }


        private void RemettreErasmeALaFin()
        {
            if (game.playerManager.nombreDeJoueurs == NombreDeJoueurs.solo)
            {
                game.playerManager.premierJoueur.RemettreErasmeALaFin();
            }
            else
            {
                game.playerManager.premierJoueur.RemettreErasmeALaFin();
                game.playerManager.deuxiemeJoueur.RemettreErasmeALaFin();
            }
            game.mechantManager.SupprimerTousLesMechants();
        }

        private void RemettreErasmeAuDebut()
        {
            if (game.playerManager.nombreDeJoueurs == NombreDeJoueurs.solo)
            {
                game.playerManager.premierJoueur.RemettreErasmeAuDebut();
            }
            else
            {
                game.playerManager.premierJoueur.RemettreErasmeAuDebut();
                game.playerManager.deuxiemeJoueur.RemettreErasmeAuDebut();
            }
            game.mechantManager.SupprimerTousLesMechants();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background.Sprite, viewportRect, Color.White);
        }
    }
}
