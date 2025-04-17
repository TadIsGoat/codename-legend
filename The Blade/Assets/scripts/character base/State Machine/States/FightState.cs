using UnityEngine;

public class FightState : State
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private NavigateState navigateState;
    [SerializeField] private AttackState attackState;
    public GameObject objectToAttack;
    [SerializeField][Tooltip("How close will the enemy get before striking")] public float attackingRange = 1f;
    [SerializeField][Tooltip("How far from the destination is considered as \"there\"")] public float destinationTreshhold = 0.1f;

    public override void Enter()
    {
        navigateState.SetUp(data.maxRunSpeed, attackingRange);
        stateMachine.Set(navigateState, true);
    }

    public override void Do()
    {
        if (stateMachine.state == navigateState) {
            navigateState.destination = objectToAttack.transform.position;

            if (navigateState.isComplete){
                attackState.target = objectToAttack.transform.position;
                stateMachine.Set(attackState, true);
            }
        }
        else if (stateMachine.state == attackState) {
            if (attackState.isComplete) {
                navigateState.SetUp(data.maxRunSpeed, attackingRange);
                stateMachine.Set(navigateState);
            }
        }
    }

    public override void Exit()
    {

    }
}
