using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DirectionSensor directionSensor;
    private string currentAnim;
    private string newAnim;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        directionSensor = GetComponentInParent<DirectionSensor>();
    }

    public void SetAnimation(Dictionary<Data.Directions, Data.Anims> animDic)
    {
        Data.Directions direction = directionSensor.GetDirection();

        if (direction == Data.Directions.W || direction == Data.Directions.NW || direction == Data.Directions.SW)
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

        newAnim = animDic[direction].ToString();

        if (currentAnim != newAnim)
        {
            animator.Play(newAnim);
            currentAnim = newAnim;
        }
    }
}
