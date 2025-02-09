using UnityEngine;

public class DirectionSensor : MonoBehaviour
{
    [SerializeField] private CharacterData.Directions lastDirection;
    private Vector2 movement;
    private Rigidbody rb;

    private void Start()
    {
        SetDirection(CharacterData.Directions.S);
    }

    public CharacterData.Directions GetDirection()
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
            lastDirection = CharacterData.Directions.NE;
        }
        else if (movement.x < -bufferValue && movement.y > bufferValue)
        {
            lastDirection = CharacterData.Directions.NW;
        }
        else if (movement.x > bufferValue && movement.y < -bufferValue)
        {
            lastDirection = CharacterData.Directions.SE;
        }
        else if (movement.x < -bufferValue && movement.y < -bufferValue)
        {
            lastDirection = CharacterData.Directions.SW;
        }
        else if (movement.x > bufferValue)
        {
            lastDirection = CharacterData.Directions.E;
        }
        else if (movement.x < -bufferValue)
        {
            lastDirection = CharacterData.Directions.W;
        }
        else if (movement.y > bufferValue)
        {
            lastDirection = CharacterData.Directions.N;
        }
        else if (movement.y < -bufferValue)
        {
            lastDirection = CharacterData.Directions.S;
        }
        return lastDirection;
    }

    public void SetDirection(CharacterData.Directions newDirection) 
    {
        lastDirection = newDirection;
    }
}
