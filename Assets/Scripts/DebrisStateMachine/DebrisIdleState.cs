using UnityEngine;

public class DebrisIdleState : DebrisBaseState
{
    public override void OnTriggerStay(DebrisStateManager debris, Collider other)
    {
        if (debris.player.isSucking)
        {
            debris.SwitchState(debris.suckedState);
        }
    }
}