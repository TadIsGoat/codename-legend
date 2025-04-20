using UnityEngine;

public static class GameData
{
    public static int animTimeMultiplier = 400; //should be set to 1000 when I figure out how to change attack lengths n shit //also replace in scene loader when fixed

    //playtime
    public enum SceneList
    {
        MainMenu,
        BaseScene,
        persistentObjects,
        final
    }

    public enum ActionMapList {
        Player,
        UI,
        Persistent,
    }
}
