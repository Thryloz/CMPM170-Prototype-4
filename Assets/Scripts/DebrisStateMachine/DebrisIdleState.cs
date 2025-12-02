using UnityEngine;

public class DebrisIdleState : DebrisBaseState
{
    public override void EnterState(DebrisStateManager debris)
    {
        debris.GetComponent<Rigidbody>().useGravity = true;
    }


    //public override void OnTriggerStay(DebrisStateManager debris, Collider other)
    //{
    //    if (debris.player.isSucking)
    //    {
    //        if (other.gameObject.CompareTag("Player"))
    //            debris.SwitchState(debris.suckedState);
    //    }   
    //}
}