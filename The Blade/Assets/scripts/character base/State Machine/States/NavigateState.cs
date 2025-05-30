using UnityEngine;

public class NavigateState : State
{
    public Vector2 destination;
    [SerializeField] private EnemyData enemyData;
    [HideInInspector] private Vector2 targetSpeed = Vector2.zero;
    public float destinationTreshhold {get; private set;} = 0f;
    private float navigatingSpeed;

    public override void Enter()
    {
        characterAnimator.SetAnimation(data.walkAnims);
    }

    public override void Do()
    {
        if(Vector2.Distance(core.transform.position, destination) < destinationTreshhold)
        {
            targetSpeed = Vector2.zero;
            isComplete = true;
        }
        else
        {
            Vector2 RelativeDestination = (destination - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(RelativeDestination.y, RelativeDestination.x) * Mathf.Rad2Deg;

            targetSpeed = Helper.AngleToVector2(angle) * navigatingSpeed;
        }
    }

    public override void FixedDo()
    {
        characterAnimator.SetAnimation(data.walkAnims); //needs to be called over time cuz the direction can change more often than the state

        targetSpeed = Vector2.Lerp(rb.linearVelocity, targetSpeed, data.lerpValue);

        Vector2 accelRate = new Vector2(
            Mathf.Abs(targetSpeed.x) > data.maxRunSpeed * data.deccelTreshhold ? data.runAccel : data.runDeccel,
            Mathf.Abs(targetSpeed.y) > data.maxRunSpeed * data.deccelTreshhold ? data.runAccel : data.runDeccel
        );

        Vector2 speedDiff = targetSpeed - rb.linearVelocity;
        Vector2 movement = speedDiff * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
    }

    public override void Exit()
    {

    }

    public void SetUp(float _navigatingSpeed, float _destinationTreshhold) {
        destinationTreshhold = _destinationTreshhold;
        navigatingSpeed = _navigatingSpeed;
    }
}