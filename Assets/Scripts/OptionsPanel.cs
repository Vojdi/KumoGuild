using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    static OptionsPanel instance;
    public static OptionsPanel Instance => instance;
    int state;
    float time;
    bool sure;
    [SerializeField] TMPro.TMP_Text quitText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Slider slider;
    private void Awake()
    {
        sure = false;
        instance = this;
        state = 0;
    }
    public void Decide()
    {
        if (state == 0)
        {
            slider.value = audioSource.volume;
            state = 1;
            time = Time.timeScale;
            Time.timeScale = 0;
        }
        else if (state == 1) {
            state = 0;
            Time.timeScale = time;
            sure = false;
            quitText.text = "quit";
            gameObject.SetActive(false);
            
        }

    }
    public void SliderChanged()
    {
        audioSource.volume = slider.value;
    }
    public void Quit()
    {
        if (!sure) {
            sure = true;
            quitText.text = "sure?";
        }
        else
        {
            Time.timeScale = 1;
            PlayerPrefs.SetFloat("audio", audioSource.volume);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
