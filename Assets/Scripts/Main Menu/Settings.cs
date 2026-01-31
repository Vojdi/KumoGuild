using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject mainMenuGj;
    [SerializeField] GameObject settingsGj;

    [SerializeField] OnTextHover returnButton;

    [SerializeField] Slider musicSlider;
    [SerializeField] AudioSource musicSource;

    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;

    public void ToReturnButtonClick()
    {
       settingsGj.SetActive(false);
       mainMenuGj.SetActive(true);
    }
    public void OnMusicSliderValueChanged()
    {
        musicSource.volume = musicSlider.value;
    }
    public void OnResolutionDropdownChanged()
    {
        string resolution = resolutionDropdown.options[resolutionDropdown.value].text;
        ChangedResolution(resolution);
    }
    void ChangedResolution(string resolution)
    {
      
        String[] parts = resolution.Split("x");
        int width = int.Parse(parts[0]);
        int height = int.Parse(parts[1]);
        if (fullscreenToggle.isOn)
        {
            Screen.SetResolution(width, height, Screen.fullScreen);
        }
        else
        {
            Screen.SetResolution(width, height, FullScreenMode.Windowed);
        }
    }
}
