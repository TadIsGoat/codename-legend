using UnityEngine;

public class EnemyController : Core
{
    [Header("States")]
    public IdleState idleState;

    [Header("dee · buh · guhng")]
    [SerializeField] private State currentState;
    [SerializeField] private float currentSpeed;

    private void Start()
    {
        SetupInstances();
    }

    private void Update()
    {
        currentSpeed = rb.linearVelocity.magnitude; //dee · buh · guhng

        SetState();
        stateMachine.state.Do();
    }

    private void SetState()
    {
        /*if (attackTask != null && attackLock.CurrentCount == 0)
        {
            stateMachine.Set(attackState);
            currentState = attackState; //dee · buh · guhng
        }
        else if (movementInput != Vector2.zero)
        {
            stateMachine.Set(walkState);
            currentState = walkState; //dee · buh · guhng
        }
        else
        {
            stateMachine.Set(idleState);
            currentState = idleState; //dee · buh · guhng
        }*/
    }
}
