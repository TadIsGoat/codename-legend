using UnityEngine;

public class NavigateState : State
{
    public Vector2 destination;
    [SerializeField][Tooltip("How far from the destination is considered as \"there\"")] private float treshhold = 0.1f;


    public override void Enter()
    {
        
    }

    public override void Do()
    {

    }

    public override void Exit()
    {

    }
}