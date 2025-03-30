using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class PatrolState : State
{
    public IdleState idleState;
    public NavigateState navigateState;
    public Transform anchor;
    [SerializeField][Tooltip("The range in which the character will be patrolling")][Range(0, 100)] private float radius = 5f;
    [SerializeField][Tooltip("How long will the character stay idle till it starts patrolling again")][Range(0, 10)] private float idleTime = 1f;

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
        float randomRadius = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        navigateState.destination = new Vector2(anchor.position.x + randomRadius * Mathf.Cos(angle), anchor.position.y + randomRadius * Mathf.Sin(angle));
        Set(navigateState, true);
    }
}
