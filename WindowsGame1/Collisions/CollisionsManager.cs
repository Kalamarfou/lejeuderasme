﻿using System;
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
            //le mechant touche erasme
            foreach (PersonnageVulnerableBox personnageVulnerableBox in personnagesVulnerablesBoxes)
            {
                if (personnageVulnerableBox.ErasmeManager.clignote == false)
                {
                    foreach (MechantAttaqueBox mechantAttaqueBox in mechantsAttaquesBoxes)
                    {
                        if (personnageVulnerableBox.Box.Intersects(mechantAttaqueBox.Box))
                        {
                            personnageVulnerableBox.ErasmeManager.soundManager.Outch();
                            mechantAttaqueBox.Mechant.mechantState = MechantState.mort;
                            personnageVulnerableBox.ErasmeManager.clignote = true;
                            personnageVulnerableBox.ErasmeManager.HeureDebutClignotage = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }
                }
            }

            //la graisse sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (GraisseAttaqueBox graisse in graisseAttaquesBoxes)
                {
                    if (graisse.Box.Intersects(mechantVulnerableBox.Box))
                    {
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                        graisse.Boule.Alive = false;
                    }
                }
            }

            //le bulo sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (Rectangle bulo in buloAttaquesBoxes)
                {
                    if (bulo.Intersects(mechantVulnerableBox.Box))
                    {
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                    }
                }
            }

            //voltaire sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (Rectangle voltaire in voltaireAttaquesBoxes)
                {
                    if (voltaire.Intersects(mechantVulnerableBox.Box))
                    {
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                    }
                }
            }

            //les explosions sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (Rectangle explosion in explosionsAttaquesBoxes)
                {
                    if (explosion.Intersects(mechantVulnerableBox.Box))
                    {
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                    }
                }
            }

            //les explosions sur erasme
            foreach (PersonnageVulnerableBox personnageVulnerableBox in personnagesVulnerablesBoxes)
            {
                if (personnageVulnerableBox.ErasmeManager.clignote == false)
                {
                    foreach (Rectangle explosion in explosionsAttaquesBoxes)
                    {
                        if (explosion.Intersects(personnageVulnerableBox.Box))
                        {
                            personnageVulnerableBox.ErasmeManager.soundManager.Outch();
                            personnageVulnerableBox.ErasmeManager.clignote = true;
                            personnageVulnerableBox.ErasmeManager.HeureDebutClignotage = gameTime.TotalGameTime.TotalMilliseconds;
                        }
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
