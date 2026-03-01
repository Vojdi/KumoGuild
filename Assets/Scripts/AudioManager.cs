using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.LowLevel;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioResource[] resources;
    private static AudioManager instance;
    public static AudioManager Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        source.resource = resources[0];
        source.Play();
    }
    public void SetBossMusic()
    {
        source.resource = resources[1];
        source.Play();
    }

}
