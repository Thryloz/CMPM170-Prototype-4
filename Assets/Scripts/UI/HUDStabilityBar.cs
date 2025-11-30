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
        EventBus.Instance.OnDamage += UpdateStabBar;
    }

    public void UpdateStabBar(GameObject target, EventBus.DamageType damageType)
    {
        if (target == player.gameObject)
            healthBar.value = player.Stability;
    }
}
