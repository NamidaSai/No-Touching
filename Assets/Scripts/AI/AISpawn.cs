using Audio;
using UnityEngine;

namespace AI
{
    public class AISpawn : MonoBehaviour
    {
        AIStats stats;

        private void Awake()
        {
            stats = GetComponent<AIStats>();
        }

        private void Start()
        {
            float newScaleValue = stats.GetScale();
            Vector3 newScale = new Vector3(newScaleValue, newScaleValue, 1f);
            transform.localScale = newScale;
        }

        public void PlaySFX(string soundName)
        {
            FindObjectOfType<AudioManager>().Play(soundName);
        }
    }
}