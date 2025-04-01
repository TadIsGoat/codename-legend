using UnityEngine;

public class EnemyController : Core
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField][Tooltip("For how long will the enemy chase 1 object")] private float chaseTimer = 5f;
    private float lastSeenTime = 0f;
    private Collider2D lastSeen;
    private Collider2D justSeen;



    [Header("States")]
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private FightState fightState;

    [Header("dee · buh · guhng")]
    [SerializeField] private float currentSpeed;

    private void Start()
    {
        SetupInstances();
        stateMachine.Set(patrolState);
    }

    private void Update()
    {
        currentSpeed = rb.linearVelocity.magnitude; //dee · buh · guhng

        lastSeenTime -= Time.deltaTime;

        CheckSurroundings();

        if (lastSeenTime > 0 && justSeen.gameObject == lastSeen.gameObject) {
            stateMachine.Set(fightState);
            fightState.objectToAttack = lastSeen.gameObject;
        }

        try
        {
            stateMachine.state.DoBranch();
        }
        catch
        {
            Debug.LogError("State might not be set correctly (try setting the state in the inspector u dumb fuck)");
        }
    }

    private void FixedUpdate()
    {
        stateMachine.state.FixedDoBranch();
    }

    private void CheckSurroundings() {
        justSeen = Physics2D.OverlapCircle(transform.position, detectionRadius);

        if (justSeen != null) {
            lastSeen = justSeen;
            lastSeenTime = chaseTimer;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
