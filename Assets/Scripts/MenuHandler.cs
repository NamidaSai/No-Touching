using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject mainMenu = default;
    [SerializeField] GameObject optionsMenu = default;
    [SerializeField] GameObject hiddenCanvas = default;
    [SerializeField] Button playButton = default;
    [SerializeField] Slider sfxVolumeSlider = default;
    [SerializeField] Slider musicVolumeSlider = default;

    SettingsHolder settings;

    private void Start()
    {
        settings = FindObjectOfType<SettingsHolder>();
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
        if (FindObjectOfType<SettingsHolder>().hasSwitchedMusic) { return; }

        FindObjectOfType<MusicPlayer>().Play("Moose");
        FindObjectOfType<SettingsHolder>().hasSwitchedMusic = true;
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