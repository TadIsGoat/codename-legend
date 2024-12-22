using UnityEngine;

public abstract class Core : MonoBehaviour
{
    [Header("Core references")]
    public Rigidbody2D rb;
    public CharacterAnimator characterAnimator;
    public StateMachine stateMachine;
    public DirectionSensor directionSensor;
    public CharacterData data;

    public void SetupInstances()
    {
        stateMachine = new StateMachine();

        State[] allChildStates = GetComponentsInChildren<State>();
        foreach (State state in allChildStates)
        {
            state.SetCore(this);
        }
    }
}