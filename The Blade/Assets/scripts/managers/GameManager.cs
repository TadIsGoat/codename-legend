using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gamePause { get; private set; } = false;
    [SerializeField] PauseMenuManager pauseMenuManager;

    public void Pause()
    {
        if (!gamePause) {
            gamePause = true;
            pauseMenuManager.ShowMenu();
        }
        else {
            gamePause = false;
            pauseMenuManager.HideMenu();
        }
    }
}