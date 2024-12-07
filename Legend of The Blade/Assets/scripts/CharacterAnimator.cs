using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    string currentAnim;
    string newAnim;

    Dictionary<Directions, Anims> idleAnims = new Dictionary<Directions, Anims>
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

    Dictionary<Directions, Anims> walkAnims = new Dictionary<Directions, Anims>
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeAnimation(States state, Directions direction)
    {
        if (direction == Directions.W || direction == Directions.NW || direction == Directions.SW)
        {
            if (!spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }

        switch (state)
        {
            case States.idle:
                newAnim = idleAnims[direction].ToString();
                break;
            case States.walking:
                newAnim = walkAnims[direction].ToString();
                break;
        }

        if (currentAnim != newAnim)
        {
            animator.Play(newAnim);
            currentAnim = newAnim;
        }
    }
}
