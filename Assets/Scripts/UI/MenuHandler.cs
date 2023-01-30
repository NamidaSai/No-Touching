using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu = default;
        [SerializeField] private GameObject optionsMenu = default;
        [SerializeField] private GameObject hiddenCanvas = default;
        [SerializeField] private Button playButton = default;
        [SerializeField] private Slider sfxVolumeSlider = default;
        [SerializeField] private Slider musicVolumeSlider = default;

        private SettingsHolder settings;
        private SettingsHolder settingsHolder;
        private MusicPlayer audioManager;

        private void Awake()
        {
            audioManager = FindObjectOfType<MusicPlayer>();
            settingsHolder = FindObjectOfType<SettingsHolder>();
        }

        private void Start()
        {
            settings = settingsHolder;
            optionsMenu.SetActive(false);
            hiddenCanvas.SetActive(false);
            AddListeners();
            ResetParameters();
        }

        private void AddListeners()
        {
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            playButton.onClick.AddListener(SwitchMusicTrack);
        }

        public void SwitchMusicTrack()
        {
            if (settingsHolder.hasSwitchedMusic) { return; }

            audioManager.Play("Moose");
            settingsHolder.hasSwitchedMusic = true;
        }

        private void ResetParameters()
        {
            sfxVolumeSlider.value = settings.GetSFXVolume();
            musicVolumeSlider.value = settings.GetMusicVolume();
        }

        public void GoToOptionsMenu()
        {
            mainMenu.SetActive(false);
            hiddenCanvas.SetActive(false);
            optionsMenu.SetActive(true);
        }

        public void GoToMainMenu()
        {
            optionsMenu.SetActive(false);
            hiddenCanvas.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void GoToHiddenCanvas()
        {
            mainMenu.SetActive(false);
            hiddenCanvas.SetActive(true);
            optionsMenu.SetActive(false);
        }

        public void SetSFXVolume(float value)
        {
            settings.SetSFXVolume(value);
        }

        public void SetMusicVolume(float value)
        {
            settings.SetMusicVolume(value);
        }
    }
}