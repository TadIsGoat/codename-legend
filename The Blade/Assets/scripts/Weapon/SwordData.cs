using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour //object-specific variables are set here
{
    [SerializeField] public bool hideOnIdle;
    [SerializeField] public float damage = 20f;
    [SerializeField] public float knockback = 20f;
    [SerializeField][Tooltip("obviously že Vector2 by taky fungoval, ale Rect má lepší visualization")] public Rect attackHitBox;
    [SerializeField][Tooltip("How much the character will move on attack")][Range(2, 100)] public float attackMovement = 20f;
    [SerializeField] public float comboTreshhold = 1f;

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

    public Dictionary<string, float> attackPoints = new Dictionary<string, float>() //time of the impact (s); u need to syns these with the animations in anim tab
    {
        {Anims.attack1.ToString(), 0.1f },
        {Anims.attack2.ToString(), 0.1f },
        {Anims.attack3.ToString(), 0.1f },
    };

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.parent.TransformPoint(attackHitBox.center), attackHitBox.size);
        }
    }
}