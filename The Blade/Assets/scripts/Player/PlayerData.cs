using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour //object-specific variables are set here
{
    public enum Directions
    {
        N,
        S,
        E,
        W,
        SW,
        SE,
        NW,
        NE
    }

    public enum Anims //west anims not needed because we can just flip the EAST anim
    {
        idle_S,
        idle_N,
        idle_E,
        idle_SE,
        idle_NE,

        walk_S,
        walk_N,
        walk_E,
        walk_SE,
        walk_NE,

        attack_S,
        attack_N,
        attack_E,
        attack_SE,
        attack_NE,
    }

    public Dictionary<Directions, Anims> idleAnims = new Dictionary<Directions, Anims>
    {
        { Directions.S, Anims.idle_S },
        { Directions.N, Anims.idle_N },
        { Directions.E, Anims.idle_E },
        { Directions.W, Anims.idle_E }, //because we can just flip the EAST anim
        { Directions.SE, Anims.idle_SE },
        { Directions.SW, Anims.idle_SE }, //because we can just flip the EAST anim
        { Directions.NE, Anims.idle_NE },
        { Directions.NW, Anims.idle_E }, //because we can just flip the EAST anim
    };

    public Dictionary<Directions, Anims> walkAnims = new Dictionary<Directions, Anims>
    {
        { Directions.S, Anims.walk_S },
        { Directions.N, Anims.walk_N },
        { Directions.E, Anims.walk_E },
        { Directions.W, Anims.walk_E }, //because we can just flip the EAST anim
        { Directions.SE, Anims.walk_SE },
        { Directions.SW, Anims.walk_SE }, //because we can just flip the EAST anim
        { Directions.NE, Anims.walk_NE },
        { Directions.NW, Anims.walk_NE }, //because we can just flip the EAST anim
    };

    public Dictionary<Directions, Anims> attackAnims = new Dictionary<Directions, Anims>
    {
        { Directions.S, Anims.attack_S },
        { Directions.N, Anims.attack_N },
        { Directions.E, Anims.attack_E },
        { Directions.W, Anims.attack_E }, //because we can just flip the EAST anim
        { Directions.SE, Anims.attack_SE },
        { Directions.SW, Anims.attack_SE }, //because we can just flip the EAST anim
        { Directions.NE, Anims.attack_NE },
        { Directions.NW, Anims.attack_NE }, //because we can just flip the EAST anim
    };
}