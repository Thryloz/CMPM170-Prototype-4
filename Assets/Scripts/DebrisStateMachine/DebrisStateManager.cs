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

    void Start()
    {
        currentState = idleState;
        player = GameManager.Instance.player.GetComponent<TempestController>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(this, other);
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
}