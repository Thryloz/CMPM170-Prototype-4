using UnityEngine;

public class DebrisOrbitingState : DebrisBaseState
{
    GameObject proj;
    Transform target;

    public override void EnterState(DebrisStateManager debris)
    {
        proj = debris.player.projectile;
        target = debris.player.transform.Find("OrbitTarget");

        //proj.transform.parent = target;
        //proj.transform.localPosition = Vector3.zero;
        //proj.transform.rotation = target.rotation;
        //proj.transform.localScale = new Vector3(3,3,3);
    }
    
    public override void UpdateState(DebrisStateManager debris)
    {
        if (target == null)
            return;

        if (!debris.player.isSucking)
        {
            debris.SwitchState(debris.projectileState);
        }
        proj.transform.SetPositionAndRotation(target.position, target.rotation);
    }

}