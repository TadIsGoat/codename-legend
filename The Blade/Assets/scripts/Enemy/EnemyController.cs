using UnityEngine;

public class EnemyController : Core
{
    [Header("States")]
    PatrolState patrolState;

    [Header("dee · buh · guhng")]
    [SerializeField] private float currentSpeed;

    private void Start()
    {
        SetupInstances();
        stateMachine.Set(patrolState);
    }

    private void Update()
    {
        currentSpeed = rb.linearVelocity.magnitude; //dee · buh · guhng

        try
        {
            stateMachine.state.DoBranch();
        }
        catch
        {
            Debug.LogError("State might not be set correctly (try setting the state in the inspector u dumb fuck)");
        }
    }

    private void FixedUpdate()
    {
        stateMachine.state.FixedDoBranch();
    }
}
