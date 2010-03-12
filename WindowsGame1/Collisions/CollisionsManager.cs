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
namespace UltimateErasme.Collisions
{
    public class CollisionsManager
    {

        public UltimateErasme game;
        public Rectangle viewportRect;
        public ArrayList personnagesVulnerablesBoxes = new ArrayList(), graisseAttaquesBoxes = new ArrayList(), buloAttaquesBoxes = new ArrayList(),
            voltaireAttaquesBoxes = new ArrayList(), transformationAttaquesBoxes = new ArrayList(), mechantsVulnerablesBoxes = new ArrayList(),
            mechantsAttaquesBoxes = new ArrayList(), explosionsAttaquesBoxes = new ArrayList();


        public CollisionsManager(UltimateErasme game, Rectangle viewportRect)
        {
            this.game = game;
            this.viewportRect = viewportRect;
        }

        //TODO
        public void Update(GameTime gameTime)
        {
            GetBoxes();
            foreach (PersonnageVulnerableBox personnageVulnerableBox in personnagesVulnerablesBoxes)
            {
                foreach (MechantAttaqueBox mechantAttaqueBox in mechantsAttaquesBoxes)
                {
                    if (personnageVulnerableBox.Box.Intersects(mechantAttaqueBox.Box))
                    {
                        personnageVulnerableBox.ErasmeManager.soundManager.Outch();
                        mechantAttaqueBox.Mechant.mechantState = MechantState.mort;
                    }
                }
            }
        }

        private void GetBoxes()
        {
            graisseAttaquesBoxes.Clear();
            buloAttaquesBoxes.Clear();
            voltaireAttaquesBoxes.Clear();
            transformationAttaquesBoxes.Clear();
            personnagesVulnerablesBoxes.Clear();
            mechantsAttaquesBoxes.Clear();
            mechantsVulnerablesBoxes.Clear();
            explosionsAttaquesBoxes.Clear();

            game.playerManager.AjouterPersonnagesVulnerablesBox(personnagesVulnerablesBoxes);
            game.playerManager.AjouterPersonnagesAttaquesBox(graisseAttaquesBoxes, voltaireAttaquesBoxes, transformationAttaquesBoxes, buloAttaquesBoxes);
            game.mechantManager.AjouterMechantVulnerablesBox(mechantsVulnerablesBoxes);
            game.mechantManager.AjouterMechantAttaquesBox(mechantsAttaquesBoxes);
            game.explosionManager.AjouterExplosionsAttaquesBox(explosionsAttaquesBoxes);
        }

    }
}
