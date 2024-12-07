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