using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("hello from jump state");
        Jump();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        if(Ctx.IsJumpPressed)
        {
            Ctx.RequireNewJumpPress = true;
        }
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
    }

    void Jump()
    {
        if (Ctx.JumpsRemaining > 0)
        {
            {
                Ctx.Rb.linearVelocityY = Ctx.jumpPower;
                Ctx.JumpsRemaining--;
                Ctx.Animator.SetTrigger("jump");
            }
//            else if (context.canceled)
//            {
//                _ctx.Rb.linearVelocityY = _ctx.Rb.linearVelocityY * 0.5f;
//                _ctx.JumpsRemaining--;
//                _ctx.Animator.SetTrigger("jump");
//            }
        }
    }
}

