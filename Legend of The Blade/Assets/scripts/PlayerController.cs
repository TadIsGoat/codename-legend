using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    [Header("Timers")]
    public float interval = 0.0001f; //better not to play with
    private float timer = 0;

    [Header("Movement")]
    [HideInInspector] public Vector2 movementInput; //input from PlayerInput class
    public float maxRunSpeed = 10f;
    private static float lerpValue = 0.5f;
    [Range(0, 10)] public float runAccel = 10f; //values outside of Range may be problematic
    [Range(0, 20)] public float runDeccel = 20f; //values outside of Range may be problematic

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            #region WASD movement
            timer = interval;

            Vector2 targetSpeed = movementInput * maxRunSpeed;
            targetSpeed = new Vector2(Mathf.Lerp(rb.linearVelocity.x, targetSpeed.x, lerpValue), Mathf.Lerp(rb.linearVelocity.y, targetSpeed.y, lerpValue));

            float accelRateX = Mathf.Abs(targetSpeed.x) > 0.01f ? runAccel: runDeccel;
            float accelRateY = Mathf.Abs(targetSpeed.y) > 0.01f ? runAccel : runDeccel;

            Vector2 accelRate = new Vector2(accelRateX, accelRateY);

            Vector2 speedDiff = targetSpeed - rb.linearVelocity;
            Vector2 movement = speedDiff * accelRate;
            rb.AddForce(movement, ForceMode2D.Force);
            #endregion
        }
    }
}