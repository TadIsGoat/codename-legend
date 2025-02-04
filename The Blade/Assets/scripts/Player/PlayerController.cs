using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

/*
 * DEAR PROGRAMMER,
 * when this script was made, only me and god knew how it works.
 * So in case you want to change something, good luck.
 * 
 * AMOUNT OF HOURS WASTED HERE: ~20
 */

public class PlayerController : Core
{
    #region VARIABLES

    private WeaponController weaponController;
    [HideInInspector] public Task attackTask; //to store the task so we can check if its running later

    [Space(17)]
    public Vector2 movementInput; //input from PlayerInput script

    [Header("States")]
    public IdleState idleState;
    public WalkState walkState;
    public AttackState attackState;

    [Header("dee · buh · guhng")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private Vector2 targetSpeed;

    #endregion

    private void Awake()
    {
        weaponController = GetComponentInChildren<WeaponController>();
    }

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

    private void FixedUpdate()
    {
        #region WASD MOVEMENT
        targetSpeed = movementInput * data.maxRunSpeed;
        targetSpeed = Vector2.Lerp(rb.linearVelocity, targetSpeed, data.lerpValue);

        Vector2 accelRate = new Vector2(
            Mathf.Abs(targetSpeed.x) > data.maxRunSpeed * 0.1f ? data.runAccel : data.runDeccel,
            Mathf.Abs(targetSpeed.y) > data.maxRunSpeed * 0.1f ? data.runAccel : data.runDeccel
        );

        Vector2 speedDiff = targetSpeed - rb.linearVelocity;
        Vector2 movement = speedDiff * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
        #endregion
    }

    public async Task Attack(Vector2 mousePos)
    {
        //CONVERTING MOUSE POSITION TO ANGLE
        Vector2 playerRelativeMousePos = (mousePos - (Vector2)transform.position).normalized; //so the position aimed for is relative to the player object, not the world center | .normalized to zaokrouhlit correctly
        float angle = Mathf.Atan2(playerRelativeMousePos.y, playerRelativeMousePos.x) * Mathf.Rad2Deg;

        //MANIPULATING THE PLAYER CHARACTER
        directionSensor.SetDirection(Helper.AngleToDirection(angle)); //set new direction depending on the angle

        //do the weapon attack task
        weaponController.attackTask = weaponController.Attack(angle, playerRelativeMousePos);
        await weaponController.attackTask;

        await Task.Yield();
    }

    private void SetState()
    {
        if (attackTask != null && attackTask.Status == TaskStatus.WaitingForActivation) //idk why, but instead of "TaskStatus.Running" u need to put this bullshit here to check if the task running
        {
            stateMachine.Set(attackState);
        }
        else if (movementInput != Vector2.zero)
        {
            stateMachine.Set(walkState);
        }
        else
        {
            stateMachine.Set(idleState);
        }
    }
}