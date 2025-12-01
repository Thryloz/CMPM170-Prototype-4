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
    public float minSize = 2f;
    public float maxSize = 5f;

    [NonSerialized] public HUDAimIndicator aimIndicator;

    void Start()
    {
        currentState = idleState;
        player = GameManager.Instance.player.GetComponent<TempestController>();
        aimIndicator = GameManager.Instance.aimIndicator;
        transform.localScale = Vector3.one * UnityEngine.Random.Range(minSize, maxSize);
        transform.rotation = UnityEngine.Random.rotation;
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