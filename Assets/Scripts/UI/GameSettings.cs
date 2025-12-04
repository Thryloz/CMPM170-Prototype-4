using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider effectsSlider;

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void AmbientVolume()
    {
        AudioManager.Instance.AmbientVolume(ambientSlider.value);
    }

    public void EffectsVolume()
    {
        AudioManager.Instance.EffectsVolume(effectsSlider.value);
    }

    public void GoBackToPause()
    {
        hudManager.SettingsToPause();
    }
}
