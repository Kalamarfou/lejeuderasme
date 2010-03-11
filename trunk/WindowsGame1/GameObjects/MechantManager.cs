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
    public class MechantManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;

        public ArrayList mechantsCollection { get; set; }

        public MechantManager(UltimateErasme game, Rectangle viewportRect)
        {
            this.viewportRect = viewportRect;
            viewportRectPlus = new Rectangle(viewportRect.X, viewportRect.Y, viewportRect.Width + 100, viewportRect.Height + 100);
            this.game = game;
            mechantsCollection = new ArrayList();
        }

        //TODO
        public void AjouterMechant()
        {
            Mechant m = new Mechant(game, viewportRect,
                new Vector2(viewportRect.Right, game.erasmeManager.jumpManager.hauteurDuSol),
                new Vector2(4, 0), 0, 1, Color.White);
            mechantsCollection.Add(m);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = mechantsCollection.Count; i > 0; i--)
            {
                if (((Mechant)mechantsCollection[i - 1]).mechantState == MechantState.mort)
	            {
                    mechantsCollection.RemoveAt(i - 1);
	            }
            }
            foreach (Mechant mechant in mechantsCollection)
            {
                mechant.Update(gameTime);
            }
            if (mechantsCollection.Count == 0)
            {
                //TODO
                AjouterMechant();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Mechant mechant in mechantsCollection)
            {
                mechant.Draw(gameTime, spriteBatch);
            }
        }
    }
}
