using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    private Rigidbody2D rb;
    private Collider2D col;
    private CharacterAnimator characterAnimator;
    private Data.Directions lastDirection = Data.Directions.S;
    //attack =============
    [HideInInspector] public bool isAttacking;
    // ===================

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

    private Data.States GetState()
    {
        if (isAttacking)
        {
            return Data.States.attacking;
        }
        else if (Mathf.Abs(rb.linearVelocity.x) > 0.5f || Mathf.Abs(rb.linearVelocity.y) > 0.5f)
        {
            return Data.States.walking;
        }
        else
        {
            return Data.States.idle;
        }
    }

    private Data.Directions GetDirection()
    {
        const float bufferValue = 0.5f;

        if (movementInput.x > bufferValue && movementInput.y > bufferValue)
        {
            lastDirection = Data.Directions.NE;
        }
        else if (movementInput.x < -bufferValue && movementInput.y > bufferValue)
        {
            lastDirection = Data.Directions.NW;
        }
        else if (movementInput.x > bufferValue && movementInput.y < -bufferValue)
        {
            lastDirection = Data.Directions.SE;
        }
        else if (movementInput.x < -bufferValue && movementInput.y < -bufferValue)
        {
            lastDirection = Data.Directions.SW;
        }
        else if (movementInput.x > bufferValue)
        {
            lastDirection = Data.Directions.E;
        }
        else if (movementInput.x < -bufferValue)
        {
            lastDirection = Data.Directions.W;
        }
        else if (movementInput.y > bufferValue)
        {
            lastDirection = Data.Directions.N;
        }
        else if (movementInput.y < -bufferValue)
        {
            lastDirection = Data.Directions.S;
        }
        return lastDirection;
    }

    #endregion
}
