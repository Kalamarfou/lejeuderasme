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
using UltimateErasme.XP;
using UltimateErasme.Life;

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
            //on remplit les boxes avec de nouvelles valeurs
            GetBoxes();

            //le mechant touche erasme
            LeMechantToucheErasme(gameTime);

            //la graisse sur le mechant
            LaGraisseToucheLeMechant(gameTime);

            //le bulo sur le mechant
            LeBuloToucheLeMechant(gameTime);

            //voltaire sur le mechant
            VoltaireToucheLeMechant(gameTime);

            //les explosions sur le mechant
            LesExplosionsSurLeMechant(gameTime);

            //les explosions sur erasme
            LesExplosionsSurErasme(gameTime);
        }

        private void LesExplosionsSurErasme(GameTime gameTime)
        {
            //les explosions sur erasme
            foreach (PersonnageVulnerableBox personnageVulnerableBox in personnagesVulnerablesBoxes)
            {
                if (personnageVulnerableBox.ErasmeManager.clignote == false)
                {
                    foreach (Rectangle explosion in explosionsAttaquesBoxes)
                    {
                        if (explosion.Intersects(personnageVulnerableBox.Box))
                        {
                            LifeManager.getInstance(game).SubstractLife(LifeEvents.SuicideALExplosion);
                            XpManager.getInstance(game).AddXp(XpEvents.SuicideALExplosion);
                            personnageVulnerableBox.ErasmeManager.soundManager.Outch();
                            personnageVulnerableBox.ErasmeManager.clignote = true;
                            personnageVulnerableBox.ErasmeManager.HeureDebutClignotage = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }
                }
            }
        }

        private void LesExplosionsSurLeMechant(GameTime gameTime)
        {
            //les explosions sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (Rectangle explosion in explosionsAttaquesBoxes)
                {
                    if (explosion.Intersects(mechantVulnerableBox.Box))
                    {
                        LifeManager.getInstance(game).AddLife(LifeEvents.KillALExplosion);
                        XpManager.getInstance(game).AddXp(XpEvents.KillALExplosion);
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                        game.playerManager.premierJoueur.soundManager.MechantMeurtExplosion();
                    }
                }
            }
        }

        private void VoltaireToucheLeMechant(GameTime gameTime)
        {
            //voltaire sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (Rectangle voltaire in voltaireAttaquesBoxes)
                {
                    if (voltaire.Intersects(mechantVulnerableBox.Box))
                    {
                        XpManager.getInstance(game).AddXp(XpEvents.KillALEclair);
                        LifeManager.getInstance(game).AddLife(LifeEvents.KillALEclair);
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                        game.playerManager.premierJoueur.soundManager.MechantMeurtVoltaire();
                    }
                }
            }
        }

        private void LeBuloToucheLeMechant(GameTime gameTime)
        {
            //le bulo sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (Rectangle bulo in buloAttaquesBoxes)
                {
                    if (bulo.Intersects(mechantVulnerableBox.Box))
                    {
                        LifeManager.getInstance(game).AddLife(LifeEvents.KillAuBulo);
                        XpManager.getInstance(game).AddXp(XpEvents.KillAuBulo);
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                        game.playerManager.premierJoueur.soundManager.MechantMeurtBulo();
                    }
                }
            }
        }

        private void LaGraisseToucheLeMechant(GameTime gameTime)
        {
            //la graisse sur le mechant
            foreach (MechantVulnerableBox mechantVulnerableBox in mechantsVulnerablesBoxes)
            {
                foreach (GraisseAttaqueBox graisse in graisseAttaquesBoxes)
                {
                    if (graisse.Box.Intersects(mechantVulnerableBox.Box))
                    {
                        LifeManager.getInstance(game).AddLife(LifeEvents.KillALaGraisse);
                        XpManager.getInstance(game).AddXp(XpEvents.KillALaGraisse);
                        mechantVulnerableBox.Mechant.mechantState = MechantState.mort;
                        graisse.Boule.Alive = false;

                        game.playerManager.premierJoueur.soundManager.MechantMeurtGraisse();
                    }
                }
            }
        }

        private void LeMechantToucheErasme(GameTime gameTime)
        {
            //le mechant touche erasme
            foreach (PersonnageVulnerableBox personnageVulnerableBox in personnagesVulnerablesBoxes)
            {
                if (personnageVulnerableBox.ErasmeManager.clignote == false)
                {
                    foreach (MechantAttaqueBox mechantAttaqueBox in mechantsAttaquesBoxes)
                    {
                        if (personnageVulnerableBox.Box.Intersects(mechantAttaqueBox.Box))
                        {
                            LifeManager.getInstance(game).SubstractLife(LifeEvents.MechantAttaque);
                            personnageVulnerableBox.ErasmeManager.soundManager.Outch();
                            mechantAttaqueBox.Mechant.mechantState = MechantState.mort;
                            personnageVulnerableBox.ErasmeManager.clignote = true;
                            personnageVulnerableBox.ErasmeManager.HeureDebutClignotage = gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }
                }
            }
        }

        private void GetBoxes()
        {
            ClearBoxes();

            game.playerManager.AjouterPersonnagesVulnerablesBox(personnagesVulnerablesBoxes);
            game.playerManager.AjouterPersonnagesAttaquesBox(graisseAttaquesBoxes, voltaireAttaquesBoxes, transformationAttaquesBoxes, buloAttaquesBoxes);
            game.mechantManager.AjouterMechantVulnerablesBox(mechantsVulnerablesBoxes);
            game.mechantManager.AjouterMechantAttaquesBox(mechantsAttaquesBoxes);
            game.explosionManager.AjouterExplosionsAttaquesBox(explosionsAttaquesBoxes);
        }

        private void ClearBoxes()
        {
            graisseAttaquesBoxes.Clear();
            buloAttaquesBoxes.Clear();
            voltaireAttaquesBoxes.Clear();
            transformationAttaquesBoxes.Clear();
            personnagesVulnerablesBoxes.Clear();
            mechantsAttaquesBoxes.Clear();
            mechantsVulnerablesBoxes.Clear();
            explosionsAttaquesBoxes.Clear();
        }

    }
}
