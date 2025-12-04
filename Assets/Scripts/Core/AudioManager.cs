using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips")]
    [SerializeField] private AudioClip bgmClip;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
    }

    private void Start()
    {
        PlayBGM();
    }

    private void PlayBGM()
    {
        musicSource.clip = bgmClip;
        musicSource.Play();
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
