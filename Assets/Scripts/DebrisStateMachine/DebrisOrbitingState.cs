using UnityEngine;

public class DebrisOrbitingState : DebrisBaseState
{
    public override void EnterState(DebrisStateManager debris)
    {
        GameObject proj = debris.player.projectile;
        Transform target = debris.player.transform.Find("OrbitTarget");

        proj.transform.parent = target;
        proj.transform.localPosition = Vector3.zero;
        proj.transform.localRotation = target.rotation;
        proj.transform.localScale = Vector3.one;
    }
    
    public override void UpdateState(DebrisStateManager debris)
    {
        if (!debris.player.isSucking)
        {
            Debug.Log("[DEBRIS] orbit state should end");
        }
    }

}