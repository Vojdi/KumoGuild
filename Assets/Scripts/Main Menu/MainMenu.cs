using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuGj;
    [SerializeField] GameObject settingsGj;
    [SerializeField] GameObject teamSelectionGj;

    [SerializeField] OnTextHover settingsButton;
    [SerializeField] OnTextHover teamSelectButton;



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
