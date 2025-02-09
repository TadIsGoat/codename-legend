using UnityEngine;

public static class Helper
{
    public static CharacterData.Directions AngleToDirection(float angle)
    {
        if (angle < 0)
            angle += 360; //normalize the angle

        if (angle >= 337.5 || angle < 22.5)
            return CharacterData.Directions.E;         // East (0° or 360°)
        else if (angle >= 22.5 && angle < 67.5)
            return CharacterData.Directions.NE;        // North-East (45°)
        else if (angle >= 67.5 && angle < 112.5)
            return CharacterData.Directions.N;         // North (90°)
        else if (angle >= 112.5 && angle < 157.5)
            return CharacterData.Directions.NW;        // North-West (135°)
        else if (angle >= 157.5 && angle < 202.5)
            return CharacterData.Directions.W;         // West (180°)
        else if (angle >= 202.5 && angle < 247.5)
            return CharacterData.Directions.SW;        // South-West (225°)
        else if (angle >= 247.5 && angle < 292.5)
            return CharacterData.Directions.S;         // South (270°)
        else // angle >= 292.5 && angle < 337.5
            return CharacterData.Directions.SE;        // South-East (315°)
    }
}
