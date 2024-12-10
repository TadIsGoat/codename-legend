using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    private Rigidbody2D rb;
    private Collider2D col;
    private CharacterAnimator characterAnimator;
    private Data.Directions lastDirection = Data.Directions.S;

    [Header("Movement")]
    [HideInInspector] public Vector2 movementInput; //input from PlayerInput script
    [SerializeField] private float maxRunSpeed = 10f;
    [SerializeField][Tooltip("If the target speed is gonna fall closer to current velocity or (max run speed * input)")][Range(0, 1)] private float lerpValue = 0.5f;
    [Range(1, 100)][SerializeField] private float runAccel = 35f; //values outside of Range may be problematic
    [Range(1, 100)][SerializeField] private float runDeccel = 20f; //values outside of Range may be problematic

    [Header("Attack")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float attackWidth = 1f;
    [SerializeField] private float damage = 20;
    [SerializeField] private float knockback = 20f;
    [SerializeField] private bool isAttacking = false;
    private Vector2 attackPoint = Vector2.zero; //temporary (hopefully)
    private Vector2 attackSize = Vector2.zero; //temporary (hopefully)
    private Animator animator;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
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

    public IEnumerator Attack(Vector2 mousePos)
    {
        isAttacking = true;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length / 2); //waits for the half of the anim - will need to change later

        attackPoint = new Vector2(transform.position.x + Mathf.Sin(Vector2.Angle(transform.position, mousePos) * Mathf.Deg2Rad) * attackRange, transform.position.y + Mathf.Cos(Vector2.Angle(transform.position, mousePos) * Mathf.Deg2Rad) * attackRange); //get the attack point in the range from the player in the angle of the mousePos
        attackSize = new Vector2(attackRange, attackWidth);

        Collider2D[] hit = Physics2D.OverlapBoxAll(Vector2.Lerp(transform.position, attackPoint, 0.5f), attackSize, Vector2.Angle(transform.position, mousePos), enemyLayer);

        foreach (Collider2D enemy in hit) 
        { 
            enemy.GetComponent<HealthScript>().TakeHit(damage, knockback, transform.position);
        }

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length / 2); //waits for the half of the anim - will need to change later

        isAttacking = false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint, attackSize);
    }
}
