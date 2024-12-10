using System.Collections.Generic;

public static class Data //for the player
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
        walk_NE
    }

    public enum States
    {
        idle,
        walking,
    }

    public static readonly Dictionary<Directions, Anims> idleAnims = new Dictionary<Directions, Anims>
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

    public static readonly Dictionary<Directions, Anims> walkAnims = new Dictionary<Directions, Anims>
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
}