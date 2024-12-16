using UnityEngine;

public static class Helper
{
    public static Data.Directions GetDirection(Vector2 movementInput, Data.Directions lastDirection, bool is8dimensional)
    {
        const float bufferValue = 0.5f;

        if (movementInput.x > bufferValue && movementInput.y > bufferValue && is8dimensional)
        {
            lastDirection = Data.Directions.NE;
        }
        else if (movementInput.x < -bufferValue && movementInput.y > bufferValue && is8dimensional)
        {
            lastDirection = Data.Directions.NW;
        }
        else if (movementInput.x > bufferValue && movementInput.y < -bufferValue && is8dimensional)
        {
            lastDirection = Data.Directions.SE;
        }
        else if (movementInput.x < -bufferValue && movementInput.y < -bufferValue && is8dimensional)
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
}