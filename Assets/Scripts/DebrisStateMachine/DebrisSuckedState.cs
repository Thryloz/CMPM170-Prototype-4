using UnityEngine;

public class DebrisSuckedState : DebrisBaseState
{
    TempestController player;
    Transform target;
    float suckSpeed;
    public override void EnterState(DebrisStateManager debris)
    {
        player = debris.player;
        target = player.transform.Find("OrbitTarget");
        suckSpeed = player.suckSpeed;
    }
    
    public override void UpdateState(DebrisStateManager debris)
    {
        // look at OrbitTarget
        Vector3 dir = (target.position - debris.transform.position).normalized;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            debris.transform.rotation = Quaternion.Slerp(debris.transform.rotation, rot, Time.deltaTime * suckSpeed);
        }

        // move to OrbitTarget
        debris.transform.position += debris.transform.forward * suckSpeed * Time.deltaTime;
    }


    public override void OnTriggerEnter(DebrisStateManager debris, Collider other)
    {
        if (other.gameObject.TryGetComponent<OrbitTargetController>(out OrbitTargetController hasTarget))
        {
            // reached final destination so become a projectile if player doens't have one
            // otherwise make it grooow
            if (player.projectile)
            {
                player.projectile.transform.localScale += Vector3.one;
                debris.DestroySelf();
            }
            else
            {
                player.projectile = debris.gameObject;
                debris.SwitchState(debris.orbitingState);
            } 
        }
    }

}