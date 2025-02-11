using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Camera cam;
    private InputAction moveAction;
    private InputAction attackAction;
    private PlayerInput playerInput;
    private GameManager gameManager;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        playerInput = GetComponent<PlayerInput>();
        gameManager = GetComponent<GameManager>();

        playerInput.actions.FindActionMap(GameData.ActionMapList.Persistent.ToString()).Enable();
    }

    private void Update()
    {
        playerController.movementInput = moveAction.ReadValue<Vector2>();

        if (attackAction.WasPerformedThisFrame())
        {
            if (Mouse.current != null)
            {
                playerController.attackTask = playerController.Attack(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
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
        playerInput.actions["Pause"].performed += SwitchActionMap;
    }

    private void OnDisable() //disables the actions when the gameObject becomes inactive => avoids memory leaks
    {
        moveAction.Disable();
        attackAction.Disable();
        playerInput.actions["Pause"].performed -= SwitchActionMap;
    }

    private void SwitchActionMap(InputAction.CallbackContext context) {
        gameManager.Pause();

        if (gameManager.gamePause){
            playerInput.actions.FindActionMap(GameData.ActionMapList.UI.ToString()).Enable();
            playerInput.actions.FindActionMap(GameData.ActionMapList.Player.ToString()).Disable();
        } else {
            playerInput.actions.FindActionMap(GameData.ActionMapList.Player.ToString()).Enable();
            playerInput.actions.FindActionMap(GameData.ActionMapList.UI.ToString()).Disable();
        }
    }
}