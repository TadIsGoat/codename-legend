using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float currentHealth = 0;
    [SerializeField] private float maxHealth = 100;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeHit(float damage, float knockback, Vector2 sourcePos)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log(currentHealth);

        rb.AddForce(-sourcePos * knockback, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
}