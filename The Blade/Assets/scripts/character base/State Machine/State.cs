using Unity.VisualScripting;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    //core and components
    protected Core core;
    protected Rigidbody2D rb => core.rb;
    protected CharacterAnimator characterAnimator => core.characterAnimator;
    protected DirectionSensor directionSensor => core.directionSensor;
    protected CharacterData data => core.data;

    //state machine tree
    public StateMachine stateMachine;
    public StateMachine parent;
    public State state => stateMachine.state;

    //functional
    public bool isComplete { get; protected set; }
    private float startTime;
    public float time => Time.time - startTime;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void DoBranch() {
        Do();
        state?.DoBranch();
    }

    public void FixedDoBranch() {
        FixedDo();
        state?.FixedDoBranch();
    }

    public void Set(State newState, bool forceReset = false)
    {
        stateMachine.Set(newState, forceReset);
    }

    public void SetCore(Core _core)
    {
        stateMachine = new StateMachine();
        core = _core;
    }

    public void Initialize(StateMachine _parent)
    {
        isComplete = false;
        startTime = Time.time;
        parent = _parent;
    }
}