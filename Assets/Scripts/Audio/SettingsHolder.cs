using UnityEngine;

namespace Audio
{
    public class SettingsHolder : MonoBehaviour
    {
        private float sfxVolume = 1f;
        private float musicVolume = 1f;


        private MusicPlayer musicPlayer;
        private AudioManager audioManager;

        public bool hasSwitchedMusic = false;

        private void Start()
        {
            musicPlayer = FindObjectOfType<MusicPlayer>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        public float GetSFXVolume()
        {
            return sfxVolume;
        }

        public float GetMusicVolume()
        {
            return musicVolume;
        }

        public void SetSFXVolume(float value)
        {
            sfxVolume = value;
            audioManager.SetVolume(value);
        }

        public void SetMusicVolume(float value)
        {
            musicVolume = value;
            musicPlayer.SetVolume(value);
        }
    }
}