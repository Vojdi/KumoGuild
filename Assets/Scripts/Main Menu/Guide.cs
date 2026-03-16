using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    [SerializeField] GameObject menuGj;
    [SerializeField] GameObject guideGj;
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] guidePages;

    Button currentButton;
    GameObject currentGuide;


    public void Return()
    {
        guideGj.SetActive(false);
        menuGj.SetActive(true);
    }
    public void ButtonClicked(int id)
    {
        currentGuide.SetActive(false);
        guidePages[id].SetActive(true);
        currentButton.enabled = true;
        currentButton.GetComponent<OnTextHover>().enabled = true;

        currentButton = buttons[id];
        currentButton.enabled = false;
        currentButton.GetComponent<OnTextHover>().enabled = false;
        currentGuide = guidePages[id];  

    }
    public void OnEnable()
    {
        currentButton = buttons[0];
        currentGuide = guidePages[0];
    }

}
