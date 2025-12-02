using System.Collections;
using UnityEngine;

public class HUDHitmarker : MonoBehaviour
{
    [SerializeField] private GameObject hitmarker; 

    void Start()
    {
        EventBus.Instance.OnDamage += RunShowHitmarker;
    }

    private void RunShowHitmarker(GameObject target, EventBus.DamageType type)
    {
        if (target.CompareTag("NPC_Tempest") && type == EventBus.DamageType.PROJECTILE)
        {
            CoroutineManager.Instance.Run(ShowHitmarker());
        }
    }

    private IEnumerator ShowHitmarker()
    {
        hitmarker.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitmarker.SetActive(false);
    }
}