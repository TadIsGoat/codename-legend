using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : CharacterData
{
    public override Dictionary<Directions, Anims> idleAnims
    {
        get => base.idleAnims;
        set => base.idleAnims = value;
    }

    public override Dictionary<Directions, Anims> walkAnims
    {
        get => base.walkAnims;
        set => base.walkAnims = value;
    }

    public override Dictionary<Directions, Anims> attackAnims
    {
        get => base.attackAnims;
        set => base.attackAnims = value;
    }

    public PlayerData()
    {
        idleAnims = new Dictionary<Directions, Anims>
        {
            { Directions.S, Anims.idle_S },
            { Directions.N, Anims.idle_N },
            { Directions.E, Anims.idle_E },
            { Directions.W, Anims.idle_E },
            { Directions.SE, Anims.idle_SE },
            { Directions.SW, Anims.idle_SE },
            { Directions.NE, Anims.idle_NE },
            { Directions.NW, Anims.idle_E },
        };

        walkAnims = new Dictionary<Directions, Anims>
        {
            { Directions.S, Anims.walk_S },
            { Directions.N, Anims.walk_N },
            { Directions.E, Anims.walk_E },
            { Directions.W, Anims.walk_E },
            { Directions.SE, Anims.walk_SE },
            { Directions.SW, Anims.walk_SE },
            { Directions.NE, Anims.walk_NE },
            { Directions.NW, Anims.walk_NE },
        };

        attackAnims = new Dictionary<Directions, Anims>
        {
            { Directions.S, Anims.attack_S },
            { Directions.N, Anims.attack_N },
            { Directions.E, Anims.attack_E },
            { Directions.W, Anims.attack_E },
            { Directions.SE, Anims.attack_SE },
            { Directions.SW, Anims.attack_SE },
            { Directions.NE, Anims.attack_NE },
            { Directions.NW, Anims.attack_NE },
        };
    }
}
