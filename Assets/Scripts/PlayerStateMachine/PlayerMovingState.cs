using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public PlayerMovingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("entered move state");
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Debug.Log("left move state");
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovePressed)
        {
            SwitchState(Factory.Idle());
        }
    }

}

