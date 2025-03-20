using System;
using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    [SerializeField] public float maxRunSpeed;
    [SerializeField] public float deccelTreshhold;
    [SerializeField] [Range(1, 100)][Tooltip("values outside of Range may be problematic")] public float runAccel;
    [SerializeField] [Range(1, 100)][Tooltip("values outside of Range may be problematic")] public float runDeccel;
    [SerializeField] [Range(0, 1)][Tooltip("If the target speed is gonna fall closer to current velocity or (max run speed * input)")] public float lerpValue;

    public virtual Directions Direction { get; set; }

    public enum Directions {

    }
}
