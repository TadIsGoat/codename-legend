using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    private WeaponData weaponData;
    private WeaponAnimator weaponAnimator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]private WeaponData.States state;
    public Task attackTask;

    //calculation variables (need to be here coz of gizmos)
    private LayerMask attackLayer;
    private Rect attackRect;
    private float angle;

    private void Awake()
    {
        weaponData = GetComponent<WeaponData>();
        weaponAnimator = GetComponentInChildren<WeaponAnimator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (weaponData.hideOnIdle)
            spriteRenderer.enabled = false;

        try
        {
            attackLayer = LayerMask.GetMask("Attackable");
        }
        catch
        {
            Debug.Log("Attackable layer mask not found");
        }
    }

    private void Update()
    {
        SetState();
        weaponAnimator.SetAnimation(angle, state);
    }

    public async Task Attack(float _angle, Vector2 playerRelativeMousePos)
    {
        angle = _angle; //needs to happen before stateChange

        await WaitForStateChange(WeaponData.States.attack); //need to wait till the state machine realizes that we wanna attack

        spriteRenderer.enabled = true;

        try
        {
            #region ATTACK CODE
            Vector2 attackCenter = (Vector2)transform.position + playerRelativeMousePos * weaponData.attackHitBox.width / 2f; //get the new center of the attack hitbox
            attackRect = new Rect(attackCenter - weaponData.attackHitBox.size / 2f, weaponData.attackHitBox.size); //get the new attack hitbox

            await Task.Delay((int)weaponAnimator.GetAnimAttackTimePoint() * 1000); //wait for the hit in the anim

            Collider2D[] hit = Physics2D.OverlapBoxAll(attackRect.center, weaponData.attackHitBox.size, angle, attackLayer);
            foreach (var target in hit)
            {
                try
                {
                    target.GetComponent<HealthScript>().TakeHit(weaponData.damage, weaponData.knockback, transform.position);
                }
                catch
                {
                    Debug.Log("HealthScript missing");
                }
            }

            await Task.Delay(((int)weaponAnimator.GetAnimLength() - (int)weaponAnimator.GetAnimAttackTimePoint()) * 1000); //wait for the rest of the anim
            #endregion
        }
        catch (Exception e)
        {
            Debug.Log($"The anim is not in the attackPoint dictionary\nException: {e}");
        }

        if (weaponData.hideOnIdle)
            spriteRenderer.enabled = false;

        await Task.Yield();
    }

    private void SetState()
    {
        if (attackTask != null && attackTask.Status != TaskStatus.RanToCompletion)
        {
            state = WeaponData.States.attack;
        }
        else
        {
            angle = 0;
            state = WeaponData.States.idle;
        }
    }

    private async Task WaitForStateChange(WeaponData.States expectedState)
    {
        while (state != expectedState)
        {
            await Task.Yield();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackRect.size != Vector2.zero && attackTask != null && attackTask.Status != TaskStatus.RanToCompletion) //to avoid trying to writeout the gizmo when not attacking
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0f, 0f, angle), Vector3.one);
            Gizmos.DrawWireCube(weaponData.attackHitBox.center, attackRect.size);
        }
    }
}