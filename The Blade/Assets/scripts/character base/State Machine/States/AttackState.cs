using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private LayerMask attackLayer;
    private Rect attackRect;
    private Vector2 relativeTarget;
    private float angle;
    private bool isAttacking = false;
    [HideInInspector] public Vector2 target;
    [SerializeField] public float damage = 20f;
    [SerializeField] public float knockback = 20f;
    [SerializeField][Tooltip("obviously �e Vector2 by taky fungoval, ale Rect m� lep�� visualization")] private Rect attackHitBox;
    [SerializeField][Tooltip("How much the character will move on attack")][Range(0, 100)] private float attackMovement = 20f;
    [SerializeField][Tooltip("How long will we wait before doing the actuall hit")][Range(0, 3)] private float attackDelay;
    [SerializeField][Tooltip("Not neccessery")] private WeaponController weaponController;

    public override void Enter()
    {
        rb.linearVelocity = Vector2.zero;

        #region calcs
        relativeTarget = (target - (Vector2)transform.position).normalized;
        angle = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg;
        
        Vector2 attackCenter = (Vector2)transform.position + relativeTarget * attackHitBox.width / 2f; //get the new center of the attack hitbox
        attackRect = new Rect(attackCenter - attackHitBox.size / 2f, attackHitBox.size); //get the new attack hitbox
        #endregion

        StartCoroutine(Attack());
    }

    public override void Do()
    {
        if (isAttacking) {
            characterAnimator.SetAnimation(data.attackAnims, true);
        } 
    }


    public override void Exit()
    {

    }

    public IEnumerator Attack() {

        yield return new WaitForSeconds(attackDelay);
        isAttacking = true;

        if (weaponController != null) //if we have a weapon we attack
        {
            StartCoroutine(weaponController.Attack(angle));
        }

        rb.AddForce(Helper.AngleToVector2(angle).normalized * attackMovement, ForceMode2D.Impulse);

        Collider2D[] hit = Physics2D.OverlapBoxAll(attackRect.center, attackHitBox.size, angle, attackLayer);
        foreach (var target in hit)
        {
            try
            {
                target.GetComponent<HealthScript>().TakeHit(damage, knockback, Helper.AngleToVector2(angle));
            }
            catch
            {
                Debug.Log("HealthScript missing");
            }
        }

        yield return new WaitForSeconds(characterAnimator.GetAnimLength());

        if (weaponController != null) { //if we have a weapon we wait for it to do its stuff
            yield return new WaitUntil(() => weaponController.isAttacking == false);
        }

        rb.linearVelocity = Vector2.zero;

        isAttacking = false;
        isComplete = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (isAttacking) //to avoid trying to writeout the gizmo when not attacking
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0f, 0f, angle), Vector3.one);
            Gizmos.DrawWireCube(attackHitBox.center, attackRect.size);
        }

        if (!Application.isPlaying)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.parent.TransformPoint(attackHitBox.center), attackHitBox.size);
        }
    }
}