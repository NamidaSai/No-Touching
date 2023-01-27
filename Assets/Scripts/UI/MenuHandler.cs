using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] GameObject mainMenu = default;
        [SerializeField] GameObject optionsMenu = default;
        [SerializeField] GameObject hiddenCanvas = default;
        [SerializeField] Button playButton = default;
        [SerializeField] Slider sfxVolumeSlider = default;
        [SerializeField] Slider musicVolumeSlider = default;

        SettingsHolder settings;
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