using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuButton : MonoBehaviour
    {
        Button button;
        private AudioManager audioManager;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PlayClickSFX);
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void PlayClickSFX()
        {
            PlaySFX("menuClick");
        }

        private void PlaySFX(string soundName)
        {
            audioManager.Play(soundName);
        }
    }
}