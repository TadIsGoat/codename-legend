using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 2)]
public class EnemyData : CharacterData
{
    [Header("Enemy detection")]
    [SerializeField][Tooltip("Enter tags that this enemy should attack")] public string[] targetList;
    [SerializeField] public float detectionRadius = 5f;
    [SerializeField][Tooltip("For how long will the enemy chase 1 object")] public float chaseTimer = 5f;

    [Header("Fight")]
    [SerializeField][Tooltip("How close will the enemy get before striking")] public float attackingRange = 1f;

    [Header("Patrol")]
    [SerializeField][Tooltip("The range in which the character will be patrolling")][Range(0, 100)] public float patrolRadius = 5f;
    [SerializeField][Tooltip("How long will the character stay idle till it starts patrolling again")][Range(0, 10)] public float idleTime = 1f;

    [Header("Navigate")]
    [SerializeField] public float navigatingSpeed = 5f;
    [SerializeField][Tooltip("How far from the destination is considered as \"there\"")] public float destinationTreshhold = 0.1f;

    [Header("Strike")]
    [SerializeField] public float damage = 20f;
    [SerializeField] public float knockback = 20f;
    [SerializeField][Tooltip("obviously �e Vector2 by taky fungoval, ale Rect m� lep�� visualization")] public Rect attackHitBox;
    [SerializeField][Tooltip("How much the character will move on attack")][Range(2, 100)] public float attackMovement = 20f;

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

    public EnemyData()
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
            { Directions.NW, Anims.idle_NE },
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
