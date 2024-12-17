using UnityEngine;

public class AttackState : State
{

    public override void Enter()
    {
        characterAnimator.SetAnimation(data.attackAnims, directionSensor.GetDirection(rb.linearVelocity));
    }

    public override void Do()
    {

    }

    public override void Exit()
    {

    }
}
