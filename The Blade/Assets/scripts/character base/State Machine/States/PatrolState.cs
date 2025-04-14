using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private NavigateState navigateState;
    [SerializeField] private Transform anchor;
    [SerializeField] private EnemyData enemyData;
    [SerializeField][Tooltip("The range in which the character will be patrolling")][Range(0, 100)] public float patrolRadius = 5f;
    [SerializeField][Tooltip("How long will the character stay idle till it starts patrolling again")][Range(0, 10)] public float idleTime = 1f;
    public override void Enter()
    {
        GoToNext();
    }

    public override void Do()
    {
        if (stateMachine.state == navigateState) {
            if (navigateState.isComplete) {
                Set(idleState, true);
                rb.linearVelocity = Vector2.zero;
            }
        }
        else {
            if (stateMachine.state.time > idleTime) {
                GoToNext();
            }
        }
    }

    public override void Exit()
    {

    }

    private void GoToNext() {
        float angle = Random.Range(0f, Mathf.PI * 2); 
        float randomRadius = Mathf.Sqrt(Random.Range(0f, 1f)) * patrolRadius;

        navigateState.destination = new Vector2(anchor.position.x + randomRadius * Mathf.Cos(angle), anchor.position.y + randomRadius * Mathf.Sin(angle));
        navigateState.SetUp(navigateState.navigatingSpeed, navigateState.destinationTreshhold);
        Set(navigateState, true);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(anchor.position, patrolRadius);
    }
}