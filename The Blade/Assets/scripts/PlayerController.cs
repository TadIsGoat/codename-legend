using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    [Header("References")]
    private Rigidbody2D rb;
    private CharacterAnimator characterAnimator;
    private Weapon weapon;

    [HideInInspector] public bool isAttacking;

    [Header("Movement")]
    public Vector2 movementInput; //input from PlayerInput script
    public float maxRunSpeed = 10f;
    [Tooltip("If the target speed is gonna fall closer to current velocity or (max run speed * input)")][Range(0, 1)] public float lerpValue = 0.5f;
    [Range(1, 100)] public float runAccel = 35f; //values outside of Range may be problematic
    [Range(1, 100)] public float runDeccel = 20f; //values outside of Range may be problematic

    [Header("States")]
    [SerializeField] private State state;
    public IdleState idleState;
    public WalkState walkState;
    public AttackState attackState;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        idleState.Setup(rb, this, characterAnimator);
        walkState.Setup(rb, this, characterAnimator);
        attackState.Setup(rb, this, characterAnimator);
        state = idleState;
    }

    private void Update()
    {
        SetState();
        state.Do();
    }

    private void FixedUpdate()
    {
        #region WASD MOVEMENT
        Vector2 targetSpeed = movementInput * maxRunSpeed;
        targetSpeed = Vector2.Lerp(rb.linearVelocity, targetSpeed, lerpValue);

        Vector2 accelRate = new Vector2(Mathf.Abs(targetSpeed.x) > 1 ? runAccel : runDeccel, Mathf.Abs(targetSpeed.y) > 1 ? runAccel : runDeccel);

        Vector2 speedDiff = targetSpeed - rb.linearVelocity;
        Vector2 movement = speedDiff * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
        #endregion
    }

    public void Attack(Vector2 mousePos)
    {
        //weapon needs to have separate animator, the player attack anim needs to be set to last till weapon attack anim is playing and everything needs to fucking work
        StartCoroutine(weapon.Attack(mousePos));
    }

    private void SetState()
    {
        State oldState = state;

        if (isAttacking)
        {
            state = attackState;
        }
        if (movementInput != Vector2.zero)
        {
            state = walkState;
        }
        else
        {
            state = idleState;
        }

        if (oldState != state || oldState.isComplete)
        {
            oldState.Exit();
            state.Initialize();
            state.Enter();
        }
    }
}