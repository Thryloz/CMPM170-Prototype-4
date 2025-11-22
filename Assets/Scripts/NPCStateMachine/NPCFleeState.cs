public class NPCFleeState : NPCBaseState
{
    public override void EnterState(NPCStateManager self)
    {
        base.EnterState(self);
    }

    public override void UpdateState(NPCStateManager self)
    {
        // run from predators
        // back to idle if nothing present
    }
}