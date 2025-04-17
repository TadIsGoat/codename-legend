using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour //object-specific variables are set here
{
    [SerializeField] public bool hideOnIdle;
    [SerializeField][Tooltip("How long does it take to reset the combo")] public float comboTreshhold = 1f;

    public enum States //singular states
    {
        idle,
        attack,
    }

    public enum Anims //singular anims
    {
        idle1,
        attack1,
        attack2,
        attack3,

    }

    public static List<Anims> idleAnims = new List<Anims> //list of anims (for each state)
    {
        Anims.idle1,

    };

    public static List<Anims> attackAnims = new List<Anims> //list of anims (for each state)
    { 
        Anims.attack1,
        Anims.attack2,
        Anims.attack3,
    };

    public Dictionary<States, List<Anims>> animLists = new Dictionary<States, List<Anims>> //one list for each state
    {
        {States.idle, idleAnims },
        {States.attack, attackAnims },

    };

    public Dictionary<string, float> attackPoints = new Dictionary<string, float>() //time of the impact (s); u need to sync these with the animations in anim tab
    {
        {Anims.attack1.ToString(), 0f },
        {Anims.attack2.ToString(), 0f },
        {Anims.attack3.ToString(), 0f },
    };
}