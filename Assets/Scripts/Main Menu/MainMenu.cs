using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuGj;
    [SerializeField] GameObject settingsGj;
    [SerializeField] GameObject teamSelectionGj;

    [SerializeField] OnTextHover settingsButton;
    [SerializeField] OnTextHover teamSelectButton;
    [SerializeField] AudioSource source;
    [SerializeField] TMPro.TMP_Text winCounter;
    [SerializeField] TMPro.TMP_Text lossCounter;


    private void Start()
    {
        float ppa = PlayerPrefs.GetFloat("audio");
        if(ppa != 0)
        {
            source.volume = ppa;
        }
        Settings.Instance.PrepSound();
        
        winCounter.text = $"Wins: {PlayerPrefs.GetInt("wins").ToString()}";
        lossCounter.text = $"Losses: {PlayerPrefs.GetInt("losses").ToString()}";
    }
    public void ToSettingButtonClick()
    {
        mainMenuGj.SetActive(false);
        settingsGj.SetActive(true);
    }
    public void ToExitButtonClick()
    {
        Application.Quit();
    }
    public void ToTeamSelectButtonClick()
    {
        mainMenuGj.SetActive(false);
        teamSelectionGj.SetActive(true);
    }
}
