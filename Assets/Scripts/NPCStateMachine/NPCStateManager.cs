using System;
using System.Collections.Generic;
using UnityEngine;

/*
start wandering
if only prey then chase
if prey and predator(s), flee
if only predator, flee

*/

public class NPCStateManager : MonoBehaviour
{
    public NPCBaseState currentState;
    private FindTargets targets;

    public NPCIdleState idleState = new NPCIdleState();
    public NPCFleeState fleeState = new NPCFleeState();
    public NPCChaseState chaseState = new NPCChaseState();


    void Start()
    {
        currentState = idleState;
        targets = transform.Find("Targeting Range").GetComponent<FindTargets>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(NPCBaseState newState)
    {
        currentState = newState;
        newState.EnterState(this);
    }
}
