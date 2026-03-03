using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryDefeatPanel : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    } 
}
