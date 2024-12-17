using UnityEngine;

public class DirectionSensor : MonoBehaviour
{
    [SerializeField] private Data.Directions lastDirection;
    private Vector2 movement;
    private Rigidbody rb;

    public Data.Directions GetDirection()
    {
        if (TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            movement = playerController.movementInput;
        }
        else if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            movement = rb.linearVelocity;
        }
        else
        {
            Debug.Log("Something is really wrong with this object");
        }

        const float bufferValue = 0.5f;

        if (movement.x > bufferValue && movement.y > bufferValue)
        {
            lastDirection = Data.Directions.NE;
        }
        else if (movement.x < -bufferValue && movement.y > bufferValue)
        {
            lastDirection = Data.Directions.NW;
        }
        else if (movement.x > bufferValue && movement.y < -bufferValue)
        {
            lastDirection = Data.Directions.SE;
        }
        else if (movement.x < -bufferValue && movement.y < -bufferValue)
        {
            lastDirection = Data.Directions.SW;
        }
        else if (movement.x > bufferValue)
        {
            lastDirection = Data.Directions.E;
        }
        else if (movement.x < -bufferValue)
        {
            lastDirection = Data.Directions.W;
        }
        else if (movement.y > bufferValue)
        {
            lastDirection = Data.Directions.N;
        }
        else if (movement.y < -bufferValue)
        {
            lastDirection = Data.Directions.S;
        }
        return lastDirection;
    }

    private void Start()
    {
        lastDirection = Data.Directions.S;
    }
}
