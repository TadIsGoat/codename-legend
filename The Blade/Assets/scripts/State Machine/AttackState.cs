using UnityEngine;

public class AttackState : State
{

    public override void Enter()
    {
        characterAnimator.SetAnimation(data.attackAnims);
    }

    public override void Do()
    {

    }

    public override void Exit()
    {

    }
}
