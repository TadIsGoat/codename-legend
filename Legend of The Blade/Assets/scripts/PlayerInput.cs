using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions actions;
    [HideInInspector] public PlayerController playerController;

    private void Awake()
    {
        actions = new InputSystem_Actions();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        actions.Enable();

        actions.Player.Move.performed += MovePerformed;
        actions.Player.Move.canceled += MoveCanceled;
    }

    private void OnDisable()
    {
        actions.Player.Move.performed -= MovePerformed;
        actions.Player.Move.canceled -= MoveCanceled;

        actions.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        playerController.movementInput = context.ReadValue<Vector2>();
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        playerController.movementInput = Vector2.zero;
    }
}