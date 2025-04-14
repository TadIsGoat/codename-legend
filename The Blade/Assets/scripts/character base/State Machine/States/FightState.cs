using UnityEngine;

public class FightState : State
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private NavigateState navigateState;
    [SerializeField] private StrikeState strikeState;
    public GameObject objectToAttack;
    [SerializeField][Tooltip("How close will the enemy get before striking")] public float attackingRange = 1f;

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
                strikeState.objectToAttack = objectToAttack;
                stateMachine.Set(strikeState, true);
            }
        }
        else if (stateMachine.state == strikeState) {
            if (strikeState.isComplete) {
                navigateState.SetUp(data.maxRunSpeed, attackingRange);
                stateMachine.Set(navigateState);
            }
        }
    }

    public override void Exit()
    {

    }
}
