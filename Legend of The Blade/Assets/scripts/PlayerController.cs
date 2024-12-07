using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;
    [HideInInspector] public CharacterAnimator characterAnimator;
    private float bufferValue = 0.5f;
    private Directions lastDirection = Directions.S;

    [Header("Movement")]
    [HideInInspector] public Vector2 movementInput; //input from PlayerInput script
    [SerializeField] private float maxRunSpeed = 10f;
    [SerializeField][Tooltip("If the target speed is gonna fall closer to current velocity or (max run speed * input)")][Range(0, 1)] private float lerpValue = 0.5f;
    [Range(1, 100)][SerializeField] private float runAccel = 35f; //values outside of Range may be problematic
    [Range(1, 100)][SerializeField] private float runDeccel = 20f; //values outside of Range may be problematic

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
    }

    private void Update()
    {
        characterAnimator.ChangeAnimation(GetState(), GetDirection());
    }

    private void FixedUpdate()
    {
        #region WASD movement
        Vector2 targetSpeed = movementInput * maxRunSpeed;
        targetSpeed = Vector2.Lerp(rb.linearVelocity, targetSpeed, lerpValue);

        Vector2 accelRate = new Vector2(Mathf.Abs(targetSpeed.x) > 1 ? runAccel : runDeccel, Mathf.Abs(targetSpeed.y) > 1 ? runAccel : runDeccel);

        Vector2 speedDiff = targetSpeed - rb.linearVelocity;
        Vector2 movement = speedDiff * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
        #endregion
    }

    #region GETTERS
    private Directions GetDirection()
    {
        if (movementInput.x > bufferValue && movementInput.y > bufferValue)
        {
            lastDirection = Directions.NE;
        }
        else if (movementInput.x < -bufferValue && movementInput.y > bufferValue)
        {
            lastDirection = Directions.NW;
        }
        else if (movementInput.x > bufferValue && movementInput.y < -bufferValue)
        {
            lastDirection = Directions.SE;
        }
        else if (movementInput.x < -bufferValue && movementInput.y < -bufferValue)
        {
            lastDirection = Directions.SW;
        }
        else if (movementInput.x > bufferValue)
        {
            lastDirection = Directions.E;
        }
        else if (movementInput.x < -bufferValue)
        {
            lastDirection = Directions.W;
        }
        else if (movementInput.y > bufferValue)
        {
            lastDirection = Directions.N;
        }
        else if (movementInput.y < -bufferValue)
        {
            lastDirection = Directions.S;
        }
        return lastDirection;
    }

    private States GetState()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.5f || Mathf.Abs(rb.linearVelocity.y) > 0.5f)
        {
            return States.walking;
        }
        else
        {
            return States.idle;
        }
    }
    #endregion
}
