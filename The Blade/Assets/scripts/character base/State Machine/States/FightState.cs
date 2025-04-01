using UnityEngine;

public class FightState : State
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private NavigateState navigateState;
    [SerializeField] private StrikeState strikeState;
    public GameObject objectToAttack;

    public override void Enter()
    {
        navigateState.SetUp(data.maxRunSpeed, enemyData.attackingRange);
        stateMachine.Set(navigateState, true);
    }

    public override void Do()
    {
        if (stateMachine.state == navigateState) {
            navigateState.destination = objectToAttack.transform.position;

            if (navigateState.isComplete){
                stateMachine.Set(strikeState, true);
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (stateMachine.state == strikeState) {
            if (strikeState.isComplete) {
                navigateState.SetUp(data.maxRunSpeed, enemyData.attackingRange);
                stateMachine.Set(navigateState);
            }
        }
    }

    public override void Exit()
    {

    }
}
