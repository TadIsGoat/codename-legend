using UnityEngine;

public class WalkState : State
{
    [SerializeField][Tooltip("What speed is considered as idle")] private float bufferValue = 0.5f;
    public override void Enter()
    {

    }

    public override void Do()
    {
        characterAnimator.SetAnimation(data.walkAnims); //needs to be called over time cuz the direction can change more often than the state

        if (Mathf.Abs(rb.linearVelocity.x) < bufferValue && Mathf.Abs(rb.linearVelocity.y) < bufferValue)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
