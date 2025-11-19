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
}