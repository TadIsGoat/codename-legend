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

    public void SetAnimation(Dictionary<CharacterData.Directions, CharacterData.Anims> animDic, bool preferRB = false)
    {
        CharacterData.Directions direction = directionSensor.GetDirection(preferRB);

        #region FLIPPING
        if (direction == CharacterData.Directions.W || direction == CharacterData.Directions.NW || direction == CharacterData.Directions.SW)
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
        #endregion

        newAnim = animDic[direction].ToString();

        if (currentAnim != newAnim)
        {
            animator.Play(newAnim);
            currentAnim = newAnim;
        }
    }

    public float GetAnimLength() {
        return animator.GetCurrentAnimatorClipInfo(0).Length;
    }
}
