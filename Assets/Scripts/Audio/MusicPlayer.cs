using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    public class MusicPlayer : AudioManager
    {
        [SerializeField] string startingTrack = null;

        private Sound currentSound = null;

        private void Start()
        {
            Play(startingTrack);
        }

        public override void Play(string trackName)
        {
            Sound sound = Array.Find(sounds, trackClip => trackClip.name == trackName);

            if (sound == null)
            {
                Debug.LogWarning("Sound: " + trackName + " not found.");
                return;
            }

            if (sound.source == null)
            {
                return;
            }

            if (currentSound != null && currentSound.source.isPlaying)
            {
                if (currentSound.source == sound.source) { return; }
                currentSound.source.Stop();
                sound.source.Play();
                return;
            }

            sound.source.Play();
            currentSound = sound;
        }
    }
}