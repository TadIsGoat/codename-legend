using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private WeaponData weaponData;

    private string currentAnim;
    private string newAnim;

    private void Awake()
    {
        weaponData = GetComponentInParent<WeaponData>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetAnimation(float angle, WeaponData.Anims anim)
    {
        //anim rotation code here (depending on angle) (need to rotate the whole renderer through transform ig)
        newAnim = anim.ToString();

        if (currentAnim != newAnim)
        {
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
