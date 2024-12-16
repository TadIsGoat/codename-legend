using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        characterAnimator.SetAnimation(Data.idleAnims, Helper.GetDirection(playerController.movementInput, lastDirection, true));
    }

    public override void Do()
    {
        if (rb.linearVelocity != Vector2.zero)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
