using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;
    private InputAction moveAction;
    private InputAction attackAction;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        playerController.movementInput = moveAction.ReadValue<Vector2>();

        if (attackAction.WasPerformedThisFrame()) 
        {
            playerController.Attack();
        }
    }
    private void OnEnable() //enables the actions when the gameObject becomes active
    {
        moveAction.Enable();
        attackAction.Enable();
    }

    private void OnDisable() //disables the actions when the gameObject becomes inactive => avoids bugs
    {
        moveAction.Disable();
        attackAction.Disable();
    }
}