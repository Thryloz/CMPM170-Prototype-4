/// <summary>
/// Wanders aimlessly when no prey or predator detected
/// </summary>
public class NPCIdleState : NPCBaseState
{
    public override void EnterState(NPCStateManager self)
    {
        base.EnterState(self);
    }

    public override void UpdateState(NPCStateManager self)
    {
        // wander
        // check for prey & predators using self.targets.prey / .predator
        // run if predators exist
        // chase prey only when no predators
    }
}