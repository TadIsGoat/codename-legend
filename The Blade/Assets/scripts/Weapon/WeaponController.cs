using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    WeaponData weaponData;
    WeaponAnimator weaponAnimator;

    //calculation variables (need to be here coz of gizmos)
    private LayerMask attackLayer;
    private Rect attackRect;
    private Vector2 playerRelativeMousePos;
    private float angle;

    private void Awake()
    {
        weaponData = GetComponent<WeaponData>();
        weaponAnimator = GetComponentInChildren<WeaponAnimator>();

        try
        {
            attackLayer = LayerMask.GetMask("Attackable");
        }
        catch
        {
            Debug.Log("Attackable layer mask not found");
        }
    }

    public async Task Attack(float _angle, Vector2 _playerRelativeMousePos)
    {
        angle = _angle;
        playerRelativeMousePos = _playerRelativeMousePos;

        weaponAnimator.SetAnimation(angle, WeaponData.Anims.attack1); //enter decisions for more anims/anim list or something

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
        await Task.Yield();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackRect.size != Vector2.zero) //to avoid trying to writeout the gizmo when the value is not set yet
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0f, 0f, angle), Vector3.one);
            Gizmos.DrawWireCube(weaponData.attackHitBox.center, attackRect.size);
        }
    }
}