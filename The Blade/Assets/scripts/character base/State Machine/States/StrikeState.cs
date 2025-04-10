using System.Threading.Tasks;
using UnityEngine;

public class StrikeState : State
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Animator animator;
    private LayerMask attackLayer;
    private Rect attackRect;
    private Vector2 relativeTarget;
    public GameObject objectToAttack;
    public Task strikeTask;
    private float angle;

    public override void Enter()
    {
        characterAnimator.SetAnimation(data.attackAnims);
        rb.linearVelocity = Vector2.zero;

        try
        {
            attackLayer = LayerMask.GetMask("Player");
        }
        catch
        {
            Debug.Log("Player layer mask not found");
        }

        #region calcs
        relativeTarget = ((Vector2)objectToAttack.transform.position - (Vector2)transform.position).normalized;
        angle = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg;
        #endregion

        strikeTask = Strike(relativeTarget);
    }

    public override void Do()
    {

    }


    public override void Exit()
    {

    }

    public async Task Strike(Vector2 relativeTarget) {

        #region STRIKE CODE
        Vector2 attackCenter = (Vector2)transform.position + relativeTarget * enemyData.attackHitBox.width / 2f; //get the new center of the attack hitbox
        attackRect = new Rect(attackCenter - enemyData.attackHitBox.size / 2f, enemyData.attackHitBox.size); //get the new attack hitbox

        Collider2D[] hit = Physics2D.OverlapBoxAll(attackRect.center, enemyData.attackHitBox.size, angle, attackLayer);
        foreach (var target in hit)
        {
            try
            {
                target.GetComponent<HealthScript>().TakeHit(enemyData.damage, enemyData.knockback, transform.position);
            }
            catch
            {
                Debug.Log("HealthScript missing");
            }
        }

        rb.AddForce(Helper.AngleToVector2(angle) * enemyData.attackMovement, ForceMode2D.Impulse);

        await Task.Delay(animator.GetCurrentAnimatorClipInfo(0).Length * 1000);

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
            Gizmos.DrawWireCube(enemyData.attackHitBox.center, attackRect.size);
        }
    }
}