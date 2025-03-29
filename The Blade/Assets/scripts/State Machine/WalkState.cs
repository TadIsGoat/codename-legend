using System.Data;
using Unity.Mathematics;
using UnityEngine;

public class WalkState : State
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void Enter()
    {

    }

    public override void Do()
    {
        characterAnimator.SetAnimation(data.walkAnims); //needs to be called over time cuz the direction can change more often than the state
        //animator.speed = Mathf.Abs(math.max(rb.linearVelocityX, rb.linearVelocityY)) / data.maxRunSpeed; //tohle nikdy fungovat nebude, cuz rb velocity a maxRunSpeed je �pln� jinak odscalovan�

        if (Mathf.Abs(rb.linearVelocityX) < data.bufferValue && Mathf.Abs(rb.linearVelocityY) < data.bufferValue)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
