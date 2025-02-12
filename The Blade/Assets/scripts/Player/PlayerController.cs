using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/*
 * DEAR PROGRAMMER,
 * when this script was made, only me and god knew how it works.
 * So in case you want to change something, good luck.
 * 
 * AMOUNT OF HOURS WASTED HERE: ~40
 */

public class PlayerController : Core
{
    #region VARIABLES

    [Header("References")]
    private WeaponController weaponController;
    private WeaponData weaponData;

    [Header("Class variables")]
    public Vector2 movementInput; //input from PlayerInput script
    private bool canInput = true;
    [HideInInspector] public Task attackTask; //to store the task so we can check if its running later
    [HideInInspector] private static SemaphoreSlim attackLock = new SemaphoreSlim(1); //is this overkill? - yes; do I know what am I doing? - absolutely fucking not

    [Header("States")]
    public IdleState idleState;
    public WalkState walkState;
    public AttackState attackState;

    [Header("dee · buh · guhng")]
    [SerializeField] private State currentState;
    [SerializeField] private float currentSpeed;
    [SerializeField] private Vector2 targetSpeed;

    #endregion

    private void Awake()
    {
        weaponController = GetComponentInChildren<WeaponController>();
        weaponData = GetComponentInChildren<WeaponData>();
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

    public async Task Attack(Vector2 mousePos)
    {
        if (!await attackLock.WaitAsync(0))
            return;

        try
        {
            canInput = false;

            #region calculations
            //If pythagoras found the right angle, you can find the right baddie. Stay motivated gang
            Vector2 playerRelativeMousePos = (mousePos - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(playerRelativeMousePos.y, playerRelativeMousePos.x) * Mathf.Rad2Deg;
            #endregion

            #region character manipulation
            rb.linearVelocity = Vector2.zero;
            weaponController.attackTask = weaponController.Attack(angle, playerRelativeMousePos);
            //directionSensor.SetDirection(Helper.AngleToDirection(angle));
            rb.AddForce((Vector2)transform.position + playerRelativeMousePos * weaponData.attackMovement, ForceMode2D.Impulse);
            #endregion

            await weaponController.attackTask;
        }
        catch
        {
            Debug.Log("Something with attacking ain't right");
        }
        finally
        {
            attackLock.Release();
            canInput = true;
            await Task.Yield();
        }
    }

    private void SetState()
    {
        if (attackTask != null && attackLock.CurrentCount == 0)
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
        }
    }
}