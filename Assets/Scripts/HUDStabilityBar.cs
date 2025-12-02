using UnityEngine;
using UnityEngine.UI;

public class HUDStabilityBar : MonoBehaviour
{
    TempestMain player;
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<TempestMain>();
    }
    void Start()
    {
        healthBar.value = player.Stability;
    }

    private void Update()
    {
        healthBar.value = player.Stability;   
    }
}
