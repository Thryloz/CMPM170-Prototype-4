using UnityEngine;

public class DebrisIdleState : DebrisBaseState
{
    public override void OnTriggerStay(DebrisStateManager debris, Collider other)
    {
        
            if (debris.player.isSucking)
            {
                if (other.gameObject.CompareTag("Player"))
                    debris.SwitchState(debris.suckedState);
            }
        
    }
}