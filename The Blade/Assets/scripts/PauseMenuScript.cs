using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowMenu() {
        this.gameObject.SetActive(true);
    }

    public void HideMenu() {
        this.gameObject.SetActive(false);
    }
}