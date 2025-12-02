using UnityEngine;

public abstract class DebrisBaseState
{
    public virtual void EnterState(DebrisStateManager debris)
    {
        // Debug.Log($"[DEBRIS] state switched to {debris.currentState}");
    }
    public virtual void UpdateState(DebrisStateManager debris){}
    public virtual void OnCollisionEnter(DebrisStateManager debris, Collision other){}
    public virtual void OnTriggerEnter(DebrisStateManager debris, Collider other){}
    // public virtual void OnTriggerStay(DebrisStateManager debris, Collider other){}
    // public virtual void OnTriggerExit(DebrisStateManager debris, Collider other)
    // {
    //     if (debris.currentState == debris.projectileState || debris.currentState == debris.orbitingState)
    //         return;
            
    //     debris.SwitchState(debris.idleState);
    // }

}