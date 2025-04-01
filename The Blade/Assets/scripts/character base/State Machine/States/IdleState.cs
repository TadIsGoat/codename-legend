using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        characterAnimator.SetAnimation(data.idleAnims);
    }

    public override void Do()
    {
        if (Mathf.Abs(rb.linearVelocity.x) >= data.bufferValue || Mathf.Abs(rb.linearVelocity.y) >= data.bufferValue)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
