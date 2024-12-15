using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float knockback = 20f;
    [SerializeField] private Rect attackHitBox;

    private PlayerController playerController;
    private LayerMask attackLayer;
    private Rect attackRect;

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
        playerController.isAttacking = true;

        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        Vector2 attackCenter = (Vector2)transform.position + direction * attackHitBox.width / 2f; //center

        attackRect = new Rect(attackCenter - attackHitBox.size / 2f, attackHitBox.size); //rect

        Debug.DrawLine(transform.position, mousePos, Color.red, 0.1f);

        Collider2D[] hit = Physics2D.OverlapBoxAll(attackRect.center, attackHitBox.size, 0f, attackLayer);

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

        playerController.isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackRect.center, attackRect.size);
    }







    /*
     
    //calculation
    private float angle;



    public IEnumerator Attack(Vector3 mousePos)
    {
        playerController.isAttacking = true;

        // Calculate the direction vector from the player to the mouse
        mousePos.z = 0f; // Ensure the attack is in 2D space
        Vector3 direction = mousePos - transform.position;

        // Calculate the angle from the player to the mouse (in degrees)
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Angle in the range of -180° to 180°

        // Convert the angle to a full 360° range (0 to 360)
        if (angle < 0)
        {
            angle += 360f;
        }

        // Update the attackBox center, positioning it based on the angle in front of the player
        Vector2 offset = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * attackBox.width / 2, Mathf.Sin(angle * Mathf.Deg2Rad) * attackBox.width / 2);
        attackBox.center = (Vector2)transform.position + offset;

        // Perform collision check with the rotated attack box
        Collider2D[] hit = Physics2D.OverlapBoxAll(attackBox.center, attackBox.size, angle, attackLayer);

        // Process any hits
        foreach (Collider2D target in hit)
        {
            HealthScript health = target.GetComponent<HealthScript>();
            if (health != null)
            {
                health.TakeHit(damage, knockback, transform.position);
            }
        }

        // Wait before ending the attack
        yield return new WaitForSeconds(1f);

        playerController.isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Use Gizmos to visualize the attack box and ensure proper rotation
        Gizmos.matrix = Matrix4x4.TRS(attackBox.center, Quaternion.Euler(0f, 0f, angle), Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, attackBox.size);
    }*/
}