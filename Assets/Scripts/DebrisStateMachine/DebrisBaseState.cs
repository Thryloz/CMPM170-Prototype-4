using UnityEngine;

public abstract class DebrisBaseState
{
    public virtual void EnterState(DebrisStateManager debris)
    {
        Debug.Log($"[DEBRIS] state switched to {debris.currentState}");
    }
    public virtual void UpdateState(DebrisStateManager debris){}
    public virtual void OnTriggerEnter(DebrisStateManager debris, Collider other){}
    public virtual void OnTriggerStay(DebrisStateManager debris, Collider other){}
    public virtual void OnTriggerExit(DebrisStateManager debris, Collider other)
    {
        debris.SwitchState(debris.idleState);
    }

}