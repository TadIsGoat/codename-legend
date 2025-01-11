using System.Data;
using Unity.Mathematics;
using UnityEngine;

public class WalkState : State
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    [SerializeField][Tooltip("What speed is considered as idle")] private float bufferValue = 0.5f;
    public override void Enter()
    {

    }

    public override void Do()
    {
        characterAnimator.SetAnimation(data.walkAnims); //needs to be called over time cuz the direction can change more often than the state
        animator.speed = Mathf.Abs(math.max(rb.linearVelocityX, rb.linearVelocityY)) / data.maxRunSpeed; //tohle nikdy fungovat nebude, cuz rb velocity a maxRunSpeed je úplnì jinak odscalované

        if (Mathf.Abs(rb.linearVelocityX) < bufferValue && Mathf.Abs(rb.linearVelocityY) < bufferValue)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
