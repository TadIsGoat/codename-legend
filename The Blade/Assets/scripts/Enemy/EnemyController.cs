using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : Core
{
    private float lastSeenTime = 0f;
    private Collider2D lastSeen;
    private Collider2D justSeen;
    [SerializeField] private EnemyData enemyData;

    [Header("States")]
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private FightState fightState;
    [SerializeField] private NavigateState navigateState;

    [Header("dee · buh · guhng")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private State currentState;

    private void Start()
    {
        SetupInstances();
        stateMachine.Set(patrolState);
    }

    private void Update()
    {
        currentSpeed = rb.linearVelocity.magnitude; //dee · buh · guhng
        currentState = stateMachine.state; //dee · buh · guhng

        lastSeenTime -= Time.deltaTime;

        SelectState();

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

    private void SelectState()
    {
        CheckSurroundings();

        if (lastSeenTime > 0) {
            stateMachine.Set(fightState);
            fightState.objectToAttack = lastSeen.gameObject;
        }
        else 
        {
            stateMachine.Set(patrolState);
        }
    }

    private void CheckSurroundings() {
        justSeen = Physics2D.OverlapCircle(transform.position, enemyData.detectionRadius);

        if (justSeen != null) {
            /*if (justSeen == lastSeen) { //if we are seeing someyhing repeatedly - extend timer
                lastSeenTime = chaseTimer;
            }
            else*/ if (enemyData.targetList.Contains(justSeen.gameObject.tag)) { //if we see something new
                if (lastSeenTime <= 0) { //if we are not chasing anything, we can chase a new thing
                    lastSeen = justSeen;
                }
                lastSeenTime = enemyData.chaseTimer;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, navigateState.destinationTreshhold);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fightState.attackingRange);
    }
}
