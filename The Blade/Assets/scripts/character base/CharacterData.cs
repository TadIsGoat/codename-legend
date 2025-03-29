using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    [SerializeField] public float maxRunSpeed;
    [SerializeField] public float deccelTreshhold;
    [SerializeField] [Range(1, 100)][Tooltip("values outside of Range may be problematic")] public float runAccel;
    [SerializeField] [Range(1, 100)][Tooltip("values outside of Range may be problematic")] public float runDeccel;
    [SerializeField] [Range(0, 1)][Tooltip("If the target speed is gonna fall closer to current velocity or (max run speed * input)")] public float lerpValue;
    [SerializeField][Tooltip("What speed is considered as idle")] public float bufferValue = 0.5f;

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

    public virtual Dictionary<Directions, Anims> idleAnims { get; set; }

    public virtual Dictionary<Directions, Anims> walkAnims { get; set; }

    public virtual Dictionary<Directions, Anims> attackAnims { get; set; }
}
