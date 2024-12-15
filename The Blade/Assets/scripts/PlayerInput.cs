using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;
    private Weapon weapon;
    private InputAction moveAction;
    private InputAction attackAction;
    [SerializeField] private Camera cam;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        playerController = GetComponent<PlayerController>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        playerController.movementInput = moveAction.ReadValue<Vector2>();

        if (attackAction.WasPerformedThisFrame()) 
        {
            if (Mouse.current != null)
            {
                StartCoroutine(weapon.Attack(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue())));
            }
            else
            {
                Debug.Log("Mouse not connected!");
            }
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