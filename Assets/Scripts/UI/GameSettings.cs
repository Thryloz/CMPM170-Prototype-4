using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private Slider musicSlider;

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void GoBackToPause()
    {
        hudManager.SettingsToPause();
    }
}
