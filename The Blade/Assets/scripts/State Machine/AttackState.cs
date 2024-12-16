using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    public override void Enter()
    {
        characterAnimator.SetAnimation(Data.attackAnims, Helper.GetDirection(playerController.movementInput, lastDirection, true));
    }

    public override void Do()
    {

    }

    public override void Exit()
    {

    }
}
