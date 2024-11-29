using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement(Vector2 input)
    {
        input *= 10;
        rb.AddForce(input, ForceMode2D.Force);
    }
}
