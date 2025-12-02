using UnityEngine;
using UnityEngine.UI;

public class HUDStabilityBar : MonoBehaviour
{
    TempestMain player;
    [SerializeField] private Slider healthBar;
    void Start()
    {
        player = GameManager.Instance.player.GetComponent<TempestMain>();
        healthBar.value = player.Stability;
    }

    private void Update()
    {
        healthBar.value = player.Stability;   
    }
}
