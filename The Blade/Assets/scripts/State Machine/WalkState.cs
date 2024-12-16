using UnityEngine;

public class WalkState : State
{
    public override void Enter()
    {
    }

    public override void Do()
    {
        characterAnimator.SetAnimation(Data.walkAnims, Helper.GetDirection(playerController.movementInput, lastDirection, true)); //needs to be called over time cuz the direction can change more often than the state

        if (Mathf.Abs(rb.linearVelocity.x) < 0.5f && Mathf.Abs(rb.linearVelocity.y) < 0.5f)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
