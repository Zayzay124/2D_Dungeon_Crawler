using UnityEngine;

public class PlayerWallClingState : PlayerBaseState
{
    public PlayerWallClingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState() { }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() { }

}
