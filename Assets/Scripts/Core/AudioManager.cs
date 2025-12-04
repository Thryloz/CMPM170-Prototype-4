using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip bgmClip;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;

    private void Start()
    {
        PlayBGM();
    }

    private void PlayBGM()
    {
        musicSource.clip = bgmClip;
        musicSource.Play();
    }
}
