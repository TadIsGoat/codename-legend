using UnityEngine;

public class EnemyController : Core
{
    [Header("States")]
    public IdleState idleState;
    public WalkState walkState;

    [Header("Movement variables")]
    public Vector2 movementInput; //needs to take input from the states (i guess, i hope)
    private bool canInput = true;
    [SerializeField] private Vector2 targetSpeed;


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

        try
        {
            SetState();
            stateMachine.state.Do();
        }
        catch
        {
            Debug.LogError("State might not be set correctly (try setting the state in the inspector u dumb fuck)");
        }
    }

    private void FixedUpdate()
    {
        #region WASD movement
        if (canInput)
        {
            targetSpeed = movementInput * data.maxRunSpeed;
        }
        else
        {
            targetSpeed = Vector2.zero;
        }

        targetSpeed = Vector2.Lerp(rb.linearVelocity, targetSpeed, data.lerpValue);

        Vector2 accelRate = new Vector2(
            Mathf.Abs(targetSpeed.x) > data.maxRunSpeed * data.deccelTreshhold ? data.runAccel : data.runDeccel,
            Mathf.Abs(targetSpeed.y) > data.maxRunSpeed * data.deccelTreshhold ? data.runAccel : data.runDeccel
        );

        Vector2 speedDiff = targetSpeed - rb.linearVelocity;
        Vector2 movement = speedDiff * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
        #endregion
    }

    private void SetState()
    {
        if (rb.linearVelocity != Vector2.zero)
        {
            stateMachine.Set(walkState);
            currentState = walkState; //dee · buh · guhng
        }
        else
        {
            stateMachine.Set(idleState);
            currentState = idleState; //dee · buh · guhng
        }
    }
}
