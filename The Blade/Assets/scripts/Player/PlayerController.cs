using UnityEngine;

/*
 * DEAR PROGRAMMER,
 * when this script was made, only me and god knew how it works.
 * So in case you want to change something, good luck.
 */

public class PlayerController : Core
{
    [SerializeField] private InputManager inputManager;
    public Vector2 movementInput; //input from PlayerInput script

    [Header("States")]
    public IdleState idleState;
    public WalkState walkState;
    public AttackState attackState;

    [Header("dee · buh · guhng")]
    [SerializeField] private State currentState;
    [SerializeField] private float currentSpeed;
    [SerializeField] private Vector2 targetSpeed; //is here for dee · buh · guhng, but we need it

    private void Start()
    {
        SetupInstances();
        stateMachine.Set(idleState, true);
    }

    private void Update()
    {
        currentSpeed = rb.linearVelocity.magnitude; //dee · buh · guhng
        currentState = stateMachine.state; //dee · buh · guhng

        SetState();
        stateMachine.state.DoBranch();
    }

    private void FixedUpdate()
    {
        stateMachine.state.FixedDoBranch();

        #region WASD movement

        targetSpeed = movementInput * data.maxRunSpeed;

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

    public void Attack(Vector2 mousePos)
    {
        inputManager.canInput = false; //lock input when we start striking, unlock in SetState() when we check for attackState completition
        attackState.target = mousePos;
        stateMachine.Set(attackState, true);
    }

    private void SetState()
    {
        if (stateMachine.state == attackState)
        {
            if (attackState.isComplete) {
                inputManager.canInput = true;
                stateMachine.Set(idleState); //we gotta get out of this doommed state somehow ://
            }
        }
        else if (stateMachine.state != attackState) {
            if (Mathf.Abs(rb.linearVelocity.x) > data.bufferValue || Mathf.Abs(rb.linearVelocity.y) >= data.bufferValue)
            {
                stateMachine.Set(walkState);
            }
            else if (Mathf.Abs(rb.linearVelocityX) <= data.bufferValue && Mathf.Abs(rb.linearVelocityY) <= data.bufferValue) {
                stateMachine.Set(idleState);
            }
        }
    }
}