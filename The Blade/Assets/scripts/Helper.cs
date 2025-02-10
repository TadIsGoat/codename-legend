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

    public enum SceneList
    {
        MainMenu,
        BaseScene,
        persistentObjects,

    }
}





/*

    public static Vector2 AngleToVector2(float angle)
    {
        if (angle < 0)
            angle += 360; // Normalize the angle

        if (angle >= 337.5 || angle < 22.5)
            return Vector2.right;          // East (1, 0)
        else if (angle >= 22.5 && angle < 67.5)
            return new Vector2(1, 1).normalized; // North-East (0.707, 0.707)
        else if (angle >= 67.5 && angle < 112.5)
            return Vector2.up;            // North (0, 1)
        else if (angle >= 112.5 && angle < 157.5)
            return new Vector2(-1, 1).normalized; // North-West (-0.707, 0.707)
        else if (angle >= 157.5 && angle < 202.5)
            return Vector2.left;           // West (-1, 0)
        else if (angle >= 202.5 && angle < 247.5)
            return new Vector2(-1, -1).normalized; // South-West (-0.707, -0.707)
        else if (angle >= 247.5 && angle < 292.5)
            return Vector2.down;           // South (0, -1)
        else // angle >= 292.5 && angle < 337.5
            return new Vector2(1, -1).normalized; // South-East (0.707, -0.707)
    }

    public static Vector2 DirectionToVector2(Directions direction)
    {
        switch (direction)
        {
            case Directions.E:
                return Vector2.right;          // East (1, 0)
            case Directions.NE:
                return new Vector2(1, 1).normalized; // North-East (0.707, 0.707)
            case Directions.N:
                return Vector2.up;            // North (0, 1)
            case Directions.NW:
                return new Vector2(-1, 1).normalized; // North-West (-0.707, 0.707)
            case Directions.W:
                return Vector2.left;           // West (-1, 0)
            case Directions.SW:
                return new Vector2(-1, -1).normalized; // South-West (-0.707, -0.707)
            case Directions.S:
                return Vector2.down;           // South (0, -1)
            case Directions.SE:
                return new Vector2(1, -1).normalized; // South-East (0.707, -0.707)
            default:
                return Vector2.zero;          // Default case (0,0)
        }
    }

*/