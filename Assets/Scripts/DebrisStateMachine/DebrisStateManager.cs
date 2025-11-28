using System;
using UnityEngine;

public class DebrisStateManager : MonoBehaviour
{
    public DebrisBaseState currentState;
    public DebrisIdleState idleState = new DebrisIdleState();
    public DebrisSuckedState suckedState = new DebrisSuckedState();
    public DebrisOrbitingState orbitingState = new DebrisOrbitingState();
    public DebrisProjectileState projectileState = new DebrisProjectileState();

    [NonSerialized] public TempestController player;
    public float stabilityDamage = 20;
    public float sizeDamage = 10; // in percentage

    [NonSerialized] public HUDAimIndicator aimIndicator;

    void Start()
    {
        currentState = idleState;
        player = GameManager.Instance.player.GetComponent<TempestController>();
        aimIndicator = GameManager.Instance.aimIndicator;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnTriggerEnter(Collider other)
    {
        try{currentState.OnTriggerEnter(this, other);}catch{}
    }

    void OnTriggerStay(Collider other)
    {
        try{currentState.OnTriggerStay(this, other);}catch{}
    }

    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    public void SwitchState(DebrisBaseState newState)
    {
        currentState = newState;
        newState.EnterState(this);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward*3);
    }
}