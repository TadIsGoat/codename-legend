using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //that thing
    private PlayerController playerController;

    //attack parameter variables
    [SerializeField] private float damage = 20f;
    [SerializeField] private float knockback = 20f;
    [SerializeField][Tooltip("obviously že Vector2 by taky fungoval, ale Rect má lepší visualization")] private Rect attackHitBox;

    //calculation variables (need to be here coz of gizmos)
    private LayerMask attackLayer;
    private Rect attackRect;
    private Vector2 playerRelativeMousePos;
    private float angle;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();

        try
        {
            attackLayer = LayerMask.GetMask("Attackable");
        }
        catch
        {
            Debug.Log("Attackable layer mask not found");
        }
    }

    public IEnumerator Attack(Vector2 mousePos)
    {
        playerRelativeMousePos = (mousePos - (Vector2)transform.position).normalized; //so the position aimed for is relative to the player object, not the world center | .normalized to zaokrouhlit correctly

        Vector2 attackCenter = (Vector2)transform.position + playerRelativeMousePos * attackHitBox.width / 2f; //get the new center of the attack hitbox

        angle = Mathf.Atan2(playerRelativeMousePos.y, playerRelativeMousePos.x) * Mathf.Rad2Deg;

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

        yield return new WaitForSeconds(1f);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(attackHitBox.center, attackHitBox.size);
        }

        if (attackRect.size != Vector2.zero) //to avoid trying to writeout the gizmo when the value is not set yet
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0f, 0f, angle), Vector3.one);
            Gizmos.DrawWireCube(attackHitBox.center, attackRect.size);
        }
    }
}