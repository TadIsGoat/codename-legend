using UnityEngine;

public static class GameData
{
    public static int animTimeMultiplier = 300; //should be set to 1000 when I figure out how to change anim lengths n shit //also replace in scene loader when fixed

    //playtime
    public enum SceneList
    {
        MainMenu,
        BaseScene,
        persistentObjects,
    }

    public enum ActionMapList {
        Player,
        UI,
        Persistent,
    }
}
