using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour //object-specific variables are set here
{
    [SerializeField] public float damage = 20f;
    [SerializeField] public float knockback = 20f;
    [SerializeField][Tooltip("obviously že Vector2 by taky fungoval, ale Rect má lepší visualization")] public Rect attackHitBox;
    
    public enum Anims
    {
        attack1,

    }

    public Dictionary<string, float> attackPoints = new Dictionary<string, float>() //time of the attack hit
    {
        {Anims.attack1.ToString(), 1.2f },
    };

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(attackHitBox.center, attackHitBox.size);
        }
    }
}