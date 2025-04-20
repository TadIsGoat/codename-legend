using System.Collections;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float currentHealth {get; private set;} = 0;
    [SerializeField] private float maxHealth = 100;

    [Header("Flash")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [HideInInspector] private Color originalColor;
    [SerializeField] private Color flashColor = Color.black; //if we want the flash to be white, we would need to use shaders or something, cuz white is default for spriter renderer
    [SerializeField][Tooltip("How long does 1 flash take (s)")][Range(0, 1)] private float flashDuration = 0.2f;

    [Header("Knockback & stun")]
    [HideInInspector] private Rigidbody2D rb;
    [SerializeField][Tooltip("For how long will the object be knocking back")][Range(0, 1)] private float knockbackDuration = 0.5f;
    public bool isKnocked {get; private set;} = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        originalColor = spriteRenderer.color;
    }

    public void TakeHit(float damage, float knockback, Vector2 direction)
    {
        TakeDamage(damage);

        StartCoroutine(TakeKnockback(direction.normalized * knockback));

        StartCoroutine(Flash(flashDuration, flashColor));
    }

    private void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    private IEnumerator Flash(float duration, Color color)
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }

    public IEnumerator TakeKnockback(Vector2 force) {
        isKnocked = true;
        rb.linearVelocity = force;

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }
}