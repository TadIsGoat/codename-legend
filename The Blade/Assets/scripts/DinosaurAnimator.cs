using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private string currentAnim;
    private string newAnim;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeAnimation(enemyData.States state, enemyData.Directions direction)
    {
        if (direction == enemyData.Directions.W || direction == enemyData.Directions.NW || direction == enemyData.Directions.SW)
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

        try
        {
            switch (state)
            {
                case enemyData.States.idle:
                    newAnim = enemyData.idleAnims[direction].ToString();
                    break;
                case enemyData.States.walking:
                    newAnim = enemyData.walkAnims[direction].ToString();
                    break;
                case enemyData.States.attacking:
                    newAnim = enemyData.attackAnims[direction].ToString();
                    break;
            }
        }
        catch
        {
            Debug.Log("This gameObject is missing a state animation (probably)"); //when this gets state from the controller that the gameObject doesn't have in it's data
        }

        if (currentAnim != newAnim)
        {
            animator.Play(newAnim);
            currentAnim = newAnim;
        }
    }
}
 