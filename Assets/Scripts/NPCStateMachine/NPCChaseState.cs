public class NPCChaseState : NPCBaseState
{
    public override void EnterState(NPCStateManager self)
    {
        base.EnterState(self);
    }

    public override void UpdateState(NPCStateManager self)
    {
        // chase NPCStateManager.prey
        // unless theres 1+ predators detected.
        // back to idle if nothing present
    }
}