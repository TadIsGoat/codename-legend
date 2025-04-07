using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private NavigateState navigateState;
    [SerializeField] private Transform anchor;
    [SerializeField] private EnemyData enemyData;

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
            if (stateMachine.state.time > enemyData.idleTime) {
                GoToNext();
            }
        }
    }

    public override void Exit()
    {

    }

    private void GoToNext() {
        float angle = Random.Range(0f, Mathf.PI * 2); 
        float randomRadius = Mathf.Sqrt(Random.Range(0f, 1f)) * enemyData.patrolRadius;

        navigateState.destination = new Vector2(anchor.position.x + randomRadius * Mathf.Cos(angle), anchor.position.y + randomRadius * Mathf.Sin(angle));
        navigateState.SetUp(enemyData.navigatingSpeed, enemyData.destinationTreshhold);
        Set(navigateState, true);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(anchor.position, enemyData.patrolRadius);
    }
}