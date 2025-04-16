using System.Threading.Tasks;
using UnityEngine;

public class StrikeState : State
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask attackLayer;
    private Rect attackRect;
    private Vector2 relativeTarget;
    public Task strikeTask;
    private float angle;
    [HideInInspector] public Vector2 target;
    [SerializeField] private float attackDeccelChange;
    [SerializeField] public float damage = 20f;
    [SerializeField] public float knockback = 20f;
    [SerializeField][Tooltip("obviously �e Vector2 by taky fungoval, ale Rect m� lep�� visualization")] public Rect attackHitBox;
    [SerializeField][Tooltip("How much the character will move on attack")][Range(0, 100)] public float attackMovement = 20f;
    [SerializeField][Tooltip("Not neccessery")] private WeaponController weaponController;

    public override void Enter()
    {
        characterAnimator.SetAnimation(data.attackAnims, true);
        rb.linearVelocity = Vector2.zero;

        #region calcs
        relativeTarget = (target - (Vector2)transform.position).normalized;
        angle = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg;
        #endregion

        strikeTask = Strike(relativeTarget);

        if (weaponController != null)
        {
            weaponController.attackTask = weaponController.Attack(angle, relativeTarget);
        }

        rb.AddForce(Helper.AngleToVector2(angle) * attackMovement, ForceMode2D.Impulse);

        //change runDeccel (accel too cuz deccel doesnt work atm) so we have more control of attack movement
        data.runDeccel += attackDeccelChange;
        data.runAccel += attackDeccelChange;
    }

    public override void Do()
    {

    }


    public override void Exit()
    {
        //change runDeccel (accel too cuz deccel doesnt work atm) after attack is done
        data.runDeccel -= attackDeccelChange;
        data.runAccel -= attackDeccelChange;
    }

    public async Task Strike(Vector2 relativeTarget) {

        #region STRIKE CODE
        Vector2 attackCenter = (Vector2)transform.position + relativeTarget * attackHitBox.width / 2f; //get the new center of the attack hitbox
        attackRect = new Rect(attackCenter - attackHitBox.size / 2f, attackHitBox.size); //get the new attack hitbox

        Collider2D[] hit = Physics2D.OverlapBoxAll(attackRect.center, attackHitBox.size, angle, attackLayer);
        foreach (var target in hit)
        {
            try
            {
                target.GetComponent<HealthScript>().TakeHit(damage, knockback, transform.position);
            }
            catch
            {
                Debug.Log("HealthScript missing");
            }
        }

        await Task.Delay(animator.GetCurrentAnimatorClipInfo(0).Length * GameData.animTimeMultiplier);

        if (weaponController != null) { //if we have a weapon we wait for it to do its stuff
            await weaponController.attackTask;
        }

        await Task.Yield();

        rb.linearVelocity = Vector2.zero;

        isComplete = true;
        #endregion
    }

    private void OnDrawGizmosSelected()
    {
        if (attackRect.size != Vector2.zero && strikeTask != null && strikeTask.Status != TaskStatus.RanToCompletion) //to avoid trying to writeout the gizmo when not attacking
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