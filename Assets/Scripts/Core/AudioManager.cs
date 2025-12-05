using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips")]
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip ambienceClip;
    [SerializeField] private AudioClip gameOverSFXClip;
    [SerializeField] private AudioClip throwSFXClip;
    [SerializeField] private AudioClip rockClashSFXCLip;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource ambienceSource; // done
    public AudioSource gameOverEffectSource;
    public AudioSource throwEffectSource;
    public AudioSource rockClashSFXSource;
    

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
    }

    private void Start()
    {
        musicSource.clip = bgmClip;
        ambienceSource.clip = ambienceClip;
        throwEffectSource.clip = throwSFXClip;
        gameOverEffectSource.clip = gameOverSFXClip;
        rockClashSFXSource.clip = rockClashSFXCLip;
        
        PlayBGM();
    }

    private void PlayBGM()
    {
        ambienceSource.Play();
        musicSource.Play();
    }

    public void PlayThrowSFX()
    {
        throwEffectSource.Play();
    }
    public void PlayRockClashSFX()
    {
        rockClashSFXSource.Play();
    }

    public void PlayGameOverSFX()
    {
        musicSource.Stop();
        ambienceSource.Stop();
        throwEffectSource.Stop();
        rockClashSFXSource.Stop();
        gameOverEffectSource.Play();
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void AmbientVolume(float volume)
    {
        ambienceSource.volume = volume;
    }
    public void EffectsVolume(float volume)
    {
        throwEffectSource.volume = volume;
        gameOverEffectSource.volume = volume;
        rockClashSFXSource.volume = volume;
    }
}
