using UnityEngine;

public class CharacterAnimator : MonoBehaviour
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

    public void ChangeAnimation(Data.States state, Data.Directions direction)
    {
        if (direction == Data.Directions.W || direction == Data.Directions.NW || direction == Data.Directions.SW)
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
                case Data.States.idle:
                    newAnim = Data.idleAnims[direction].ToString();
                    break;
                case Data.States.walking:
                    newAnim = Data.walkAnims[direction].ToString();
                    break;
                case Data.States.attacking:
                    newAnim = Data.attackAnims[direction].ToString();
                    break;
            }
        }
        catch
        {
            Debug.Log("This gameObject is missing a state animation (probably)"); //when this gets state from the controller that the gameObject doesn't have anim in it's data
        }

        if (currentAnim != newAnim)
        {
            animator.Play(newAnim);
            currentAnim = newAnim;
        }
    }
}
