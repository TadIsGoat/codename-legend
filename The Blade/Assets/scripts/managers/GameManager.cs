using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gamePause { get; private set; } = false;
    [SerializeField] PauseMenuScript pauseMenuScript;

    public void Pause()
    {
        if (!gamePause) {
            gamePause = true;
            pauseMenuScript.ShowMenu();
        }
        else {
            gamePause = false;
            pauseMenuScript.HideMenu();
        }
    }
}