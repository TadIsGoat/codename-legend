using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputSystem_Actions actions;
    public PlayerController playerController;

    private void Awake()
    {
        actions = GetComponent<InputSystem_Actions>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        actions.Enable();

        actions.Player.Move.performed += MovePerformed;
        actions.Player.Move.canceled += MoveCanceled;
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {

    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {

    }
}