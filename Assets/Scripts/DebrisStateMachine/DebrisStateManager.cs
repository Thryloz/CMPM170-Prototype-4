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
    public float stabilityDamage = 10;
    public float sizePercentDamage = 10; // in percentage
    public float minSize = 2f;
    public float maxSize = 5f;

    public Rigidbody rb;

    [Header("Debug")]
    public string state;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentState = idleState;
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.GetComponent<TempestController>();

        transform.localScale = Vector3.one * UnityEngine.Random.Range(minSize, maxSize);
        transform.rotation = UnityEngine.Random.rotation;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }

    public void SwitchState(DebrisBaseState newState)
    {
        currentState = newState;
        newState.EnterState(this);
        state = newState.ToString();
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