using Unity.VisualScripting;
using UnityEngine;

public abstract class State : MonoBehaviour
{

    protected Core core;
    protected Rigidbody2D rb => core.rb;
    protected CharacterAnimator characterAnimator => core.characterAnimator;
    protected DirectionSensor directionSensor => core.directionSensor;
    protected Data data => core.data;

    public bool isComplete { get; protected set; }

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void SetCore(Core _core)
    {
        core = _core;
    }
    public void Initialize()
    {
        isComplete = false;
    }
}