using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState (PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory) {}
    public override void EnterState()
    {
        Debug.Log("entered idle state");
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (Ctx.Rb.linearVelocity.magnitude > 0.1)
        {
            SwitchState(Factory.Move());
        }
    }
}
