using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuGj;
    [SerializeField] GameObject settingsGj;

    [SerializeField] OnTextHover settingsButton;



    public void ToSettingButtonClick()
    {
        settingsButton.SetToStartSize();
        mainMenuGj.SetActive(false);
        settingsGj.SetActive(true);
    }
    public void ToExitButtonClick()
    {
        Application.Quit();
    }
}
