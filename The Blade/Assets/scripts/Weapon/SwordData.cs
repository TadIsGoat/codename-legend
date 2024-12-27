using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour //object-specific variables are set here
{
    [SerializeField] public float damage = 20f;
    [SerializeField] public float knockback = 20f;
    [SerializeField][Tooltip("obviously že Vector2 by taky fungoval, ale Rect má lepší visualization")] public Rect attackHitBox;
    
    public enum States
    {
        idle,
        attack,
    }

    public enum Anims
    {
        idle1,
        attack1,

    }

    public static List<Anims> idleAnims = new List<Anims> //might be better to store in a list so its easier to loop and stuff ???
    {
        Anims.idle1,

    };

    public static List<Anims> attackAnims = new List<Anims> //might be better to store in a list so its easier to loop and stuff ???
    { 
        Anims.attack1,

    };

    public Dictionary<States, List<Anims>> animLists = new Dictionary<States, List<Anims>>
    {
        {States.idle, idleAnims },
        {States.attack, attackAnims },

    };

    public Dictionary<string, float> attackPoints = new Dictionary<string, float>() //time of the impact
    {
        {Anims.attack1.ToString(), 0.6f },
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