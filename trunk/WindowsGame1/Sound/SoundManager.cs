using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace UltimateErasme.Sound
{
    public class SoundManager
    {
        AudioEngine moteurSonore;
        WaveBank banqueWaves;
        SoundBank banqueSons;


        public SoundManager()
        {
            moteurSonore = new AudioEngine(@"Content\Sound\jeuDErasme.xgs");
            banqueWaves = new WaveBank(moteurSonore, @"Content\Sound\Wave Bank.xwb");
            if (banqueWaves != null)
                banqueSons = new SoundBank(moteurSonore, @"Content\Sound\Sound Bank.xsb");
        }

        public void Saut()

        {
            Cue cue = banqueSons.GetCue("Saut");
            cue.Play();
        }

        public void DoubleSaut()
        {
            Cue cue = banqueSons.GetCue("DoubleSaut");
            cue.Play();
        }

        public void BuloBulo()
        {
            Cue cue = banqueSons.GetCue("bulo_sound");
            cue.Play();
        }

        public void Plop()
        {
            Cue cue = banqueSons.GetCue("plop_");
            cue.Play();
        }

        public void AttaqueVoltaire()
        {
            Cue cue = banqueSons.GetCue("voltaire_attack_sound");
            cue.Play();
        }

        public void VoltaireDisponible()
        {
            Cue cue = banqueSons.GetCue("voltaire_disponible");
            cue.Play();
        }
        public void ErasmeDisponible()
        {
            Cue cue = banqueSons.GetCue("erasme_disponible");
            cue.Play();
        }

        public void Transformation()
        {
            Cue cue = banqueSons.GetCue("thunder");
            cue.Play();
        }

        public void AttaqueErasme()
        {
            Cue cue = banqueSons.GetCue("AttaqueGraisse");
            cue.Play();
        }

        public void AttaqueErasme360()
        {
            Cue cue = banqueSons.GetCue("AttaqueGraisse360");
            cue.Play();
        }

        public void Outch()
        {
            Cue cue = banqueSons.GetCue("ErasmeTouche");
            cue.Play();
        }

        public void MechantMeurtGraisse()
        {
            Cue cue = banqueSons.GetCue("MechantMeurtGraisse");
            cue.Play();
        }

        public void MechantMeurtBulo()
        {
            Cue cue = banqueSons.GetCue("MechantMeurtBulo");
            cue.Play();
        }

        public void MechantMeurtExplosion()
        {
            Cue cue = banqueSons.GetCue("MechantMeurtExplosion");
            cue.Play();
        }

        public void MechantMeurtVoltaire()
        {
            Cue cue = banqueSons.GetCue("MechantMeurtVoltaire");
            cue.Play();
        }


        
    }
}
