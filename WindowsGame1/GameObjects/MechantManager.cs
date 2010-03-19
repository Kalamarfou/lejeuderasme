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
using UltimateErasme.Collisions;

namespace UltimateErasme.GameObjects
{
    public class MechantManager
    {
        public UltimateErasme game;
        public Rectangle viewportRect;
        public Rectangle viewportRectPlus;

        Random random = new Random();

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
            Vector2 positionDepartMechant;
            Vector2 vitesseMechant;
            float scaleMechant;
            SpriteEffects sensDuMechant;

            float temp = random.Next(1, 6);
            temp = temp / 6;
            scaleMechant = 0.5f + temp;

            if (random.Next(1,10) == 1)
            {
                vitesseMechant = new Vector2(-random.Next(1, 4), 0);
                //le Y est calculé dans le constructeur de Mechant
                positionDepartMechant = new Vector2(viewportRect.Left, 0);
                sensDuMechant = SpriteEffects.FlipHorizontally;
            }
            else
	        {
                vitesseMechant = new Vector2(random.Next(1, 4), 0);
                //le Y est calculé dans le constructeur de Mechant
                positionDepartMechant = new Vector2(viewportRect.Right, 0);
                sensDuMechant = SpriteEffects.None;
            }
            
            Mechant m = new Mechant(game, viewportRect,
                positionDepartMechant, vitesseMechant, 0, scaleMechant, sensDuMechant, Color.White);
            mechantsCollection.Add(m);
        }

        public void Update(GameTime gameTime)
        {
            SupprimerMechantsMorts();
            foreach (Mechant mechant in mechantsCollection)
            {
                mechant.Update(gameTime);
            }
            MechantsIA(gameTime);
        }

        private void MechantsIA(GameTime gameTime)
        {
            //TODO
            if (game.decorsManager.level == 1 ||
                game.decorsManager.level == 3 ||
                game.decorsManager.level == 5
                )
            {
                if (mechantsCollection.Count == 0)
                {
                    if (random.Next(1, 120) == 12)
                    {
                        AjouterMechant();
                    }
                }
                if (random.Next(1, 200) == 12)
                {
                    AjouterMechant();
                }

                foreach (Mechant mechant in mechantsCollection)
                {
                    if (((Mechant)mechantsCollection[0]).jumpManager.jumpState == JumpState.auSol)
                    {
                        if (random.Next(1, 120) == 12)
                        {
                            ((Mechant)mechantsCollection[0]).jumpManager.Sauter();
                        }
                    }
                    else if (((Mechant)mechantsCollection[0]).jumpManager.jumpState == JumpState.arriveEnHaut)
                    {
                        if (random.Next(1, 60) == 12)
                        {
                            ((Mechant)mechantsCollection[0]).jumpManager.DoubleSauter(mechant.sensDuMechant);
                        }
                    }
                }
            }
        }

        private void SupprimerMechantsMorts()
        {
            for (int i = mechantsCollection.Count; i > 0; i--)
            {
                if (((Mechant)mechantsCollection[i - 1]).mechantState == MechantState.mort)
                {
                    mechantsCollection.RemoveAt(i - 1);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Mechant mechant in mechantsCollection)
            {
                mechant.Draw(gameTime, spriteBatch);
            }
        }

        internal void AjouterMechantVulnerablesBox(ArrayList mechantsVulnerablesBox)
        {
            foreach (Mechant mechant in mechantsCollection)
            {
                Rectangle rect = new Rectangle((int)mechant.MechantGameObject.Position.X, (int)mechant.MechantGameObject.Position.Y, 
                    mechant.MechantGameObject.Sprite.Width, mechant.MechantGameObject.Sprite.Height);
                mechantsVulnerablesBox.Add(new MechantVulnerableBox(rect, mechant));
            }
        }

        internal void AjouterMechantAttaquesBox(ArrayList mechantsAttaquesBox)
        {
            foreach (Mechant mechant in mechantsCollection)
            {
                Rectangle rect = new Rectangle((int)mechant.MechantGameObject.Position.X, (int)mechant.MechantGameObject.Position.Y,
                    mechant.MechantGameObject.Sprite.Width, mechant.MechantGameObject.Sprite.Height);
                mechantsAttaquesBox.Add(new MechantAttaqueBox(rect, mechant));
            }
        }

        internal void SupprimerTousLesMechants()
        {
            mechantsCollection.Clear();
        }
    }
}
