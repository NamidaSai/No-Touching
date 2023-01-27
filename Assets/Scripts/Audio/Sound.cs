using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        public bool randomPitch;

        public bool loop;

        public bool stopOnPause = false;

        [HideInInspector]
        public AudioSource source;
    }
}