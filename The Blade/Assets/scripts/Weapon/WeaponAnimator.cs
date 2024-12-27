using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private DirectionSensor directionSensor;
    private WeaponData weaponData;

    private string currentAnim;
    private string newAnim;

    //bool facingLeft = false;

    private void Awake()
    {
        weaponData = GetComponentInParent<WeaponData>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        directionSensor = GetComponentInParent<DirectionSensor>();
    }

    public void SetAnimation(float angle, WeaponData.States state)
    {
        #region FLIPPING
        CharacterData.Directions direction = directionSensor.GetDirection();

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
        
        /*if (Mathf.Abs(angle) > 90 && facingLeft == false)
        {
            facingLeft = !facingLeft;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            scale.y *= -1;
            transform.localScale = scale;
        }*/

        List<WeaponData.Anims> animList = weaponData.animLists[state];

        newAnim = animList[0].ToString();

        if (currentAnim != newAnim)
        {
            transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.x, transform.parent.rotation.y, angle);

            animator.Play(newAnim);
            currentAnim = newAnim;
        }
    }

    public float GetAnimLength()
    {
        return animator.GetCurrentAnimatorClipInfo(0).Length;
    }

    public float GetAnimAttackTimePoint()
    {
        return weaponData.attackPoints[currentAnim];
    }
}
