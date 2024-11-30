using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Vector2 movementInput;

    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.AddForce(movementInput, ForceMode2D.Force);
    }

    public void Movement()
    {
        
    }
}
