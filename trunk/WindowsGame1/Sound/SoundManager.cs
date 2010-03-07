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
            Cue cue = banqueSons.GetCue("blup");
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
            Cue cue = banqueSons.GetCue("splurtch");
            cue.Play();
        }

        public void AttaqueErasme360()
        {
            Cue cue = banqueSons.GetCue("splurtch360");
            cue.Play();
        }
    }
}
