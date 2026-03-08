using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class VictoryDefeatPanel : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] string prefKey;
    public void BackToMenu()
    {
        int count = PlayerPrefs.GetInt(prefKey);
        count++;
        PlayerPrefs.SetInt(prefKey, count);
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("audio", audioSource.volume);
        SceneManager.LoadScene("MainMenu");
    } 
}
