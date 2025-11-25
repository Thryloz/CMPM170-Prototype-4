using UnityEngine;
using UnityEngine.Rendering;

public class DebrisProjectileState : DebrisBaseState
{
    public override void EnterState(DebrisStateManager debris)
    {
        Debug.Log("[DEBRIS] now a projectile");
        debris.transform.parent = null;
        debris.player.projectile = null;
    }
    
    public override void UpdateState(DebrisStateManager debris)
    {
        debris.transform.position += debris.transform.forward * 100 * Time.deltaTime;
    }

    public override void OnTriggerEnter(DebrisStateManager debris, Collider other)
    {
        if (other.CompareTag("NPC_Tempest"))
        {
            TempestMain tempest = other.GetComponent<TempestMain>();
            tempest.size *= 1 - debris.sizeDamage/100; 
            tempest.Stability -= debris.stabilityDamage;
        }
    }
}