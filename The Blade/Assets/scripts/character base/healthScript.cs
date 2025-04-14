using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] public float currentHealth {get; private set;} = 0;
    [SerializeField] private float maxHealth = 100;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeHit(float damage, float knockback, Vector2 sourcePos)
    {
        #region health handling
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
        #endregion

        #region knockback handling
        Vector2 relativeSource = sourcePos - (Vector2)transform.position;
        rb.AddForce(-relativeSource * knockback, ForceMode2D.Impulse);
        #endregion
    }
}