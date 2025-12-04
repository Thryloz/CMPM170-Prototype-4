using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips")]
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip ambienceClip;
    [SerializeField] private AudioClip treeBreakSFX;
    [SerializeField] private AudioClip deathSFXClip;
    [SerializeField] private AudioClip throwSFXClip;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource ambienceSource; // done
    public AudioSource treeBreakSource; // 
    public AudioSource deathEffectSource;
    public AudioSource throwEffectSource;
    

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
        ambienceSource.clip = ambienceClip;
        throwEffectSource.clip = throwSFXClip;

        ambienceSource.Play();
        musicSource.Play();
    }

    public void PauseBGM()
    {
        musicSource.Stop();
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
    }

    public IEnumerator DelayMusic()
    {
        yield return new WaitForSeconds(1);
    }
}
