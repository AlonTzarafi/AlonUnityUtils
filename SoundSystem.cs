using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlonUnityUtils
{
    public class SoundSystem
    {
        private AudioSource audioSource;
        private SoundConfig soundConfig;

        public SoundSystem(SoundConfig soundConfig)
        {
            var audioSourceGameObject = new GameObject();
            audioSource = audioSourceGameObject.AddComponent<AudioSource>();
            this.soundConfig = soundConfig;
        }

        public void PlaySound(string soundName)
        {
            soundName = soundName.Trim();

            if (soundName == null || soundName.Length == 0) {
                // Empty sound / No sound
                return;
            }

            var clip = soundConfig.GetClip(soundName);
            if (clip != null) {
                audioSource.pitch = 1f;
                audioSource.PlayOneShot(clip);
            }
        }
        
        public void PlaySoundWithSpecialPitchBasic(string soundName, float pitch)
        {
            soundName = soundName.Trim();

            if (soundName == null || soundName.Length == 0) {
                // Empty sound / No sound
                return;
            }

            var clip = soundConfig.GetClip(soundName);
            if (clip != null) {
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(clip);
            }
        }
        
    }
}
