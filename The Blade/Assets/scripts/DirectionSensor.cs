using UnityEngine;

public class DirectionSensor : MonoBehaviour
{
    public Data.Directions lastDirection;

    public Data.Directions GetDirection(Vector2 movement)
    {
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
