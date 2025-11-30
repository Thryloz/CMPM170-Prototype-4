using System.Collections;
using UnityEngine;

public class HUDHitmarker : MonoBehaviour
{
    [SerializeField] private GameObject hitmarker; 

    void Start()
    {
        EventBus.Instance.OnDamage += RunShowHitmarker;
    }

    private void RunShowHitmarker(GameObject target)
    {
        CoroutineManager.Instance.Run(ShowHitmarker(target));
    }

    private IEnumerator ShowHitmarker(GameObject BallsMcGee)
    {
        hitmarker.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitmarker.SetActive(false);
    }
}