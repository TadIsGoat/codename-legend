using Unity.VisualScripting;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    public Data.Directions lastDirection { get; protected set; }

    //following need to be set in Setup()
    protected PlayerController playerController;
    protected Rigidbody2D rb;
    protected CharacterAnimator characterAnimator;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void Setup(Rigidbody2D _rb, PlayerController _playerController, CharacterAnimator _characterAnimator)
    {
        rb = _rb;
        playerController = _playerController;
        characterAnimator = _characterAnimator;
    }

    public void Initialize()
    {
        isComplete = false;
    }

    private void Start()
    {
        lastDirection = Data.Directions.S;
    }
}