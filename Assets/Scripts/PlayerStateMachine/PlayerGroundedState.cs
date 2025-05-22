using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base (currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("entered grounded state");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.JumpsRemaining = Ctx.MaxJumps;
        Debug.Log("left grounded state");
    }

    public override void InitializeSubState()
    {
        //*rb.magnitude is less than 0.1
        if (!Ctx.IsMovePressed) {
            SetSubState(Factory.Idle());
        } //rb.magnitude is greater than 0.1
        else if (Ctx.IsMovePressed) {
            SetSubState(Factory.Move());
/*        } else if () {
            SetSubState(_factory.Dash());*/
        }
    }

    public override void CheckSwitchStates()
    {
        //if player is grounded and jump is pressed, switch to jump state
        if(Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
        }
    }

}

