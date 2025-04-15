using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    /*

    Default button color: FAFBF6 (250, 251, 246)
    Highlighted button color: C6B7BE (198, 183, 190)

    */
 
    [SerializeField] private EventSystem eventSystem;

    [Header("Button animation")]
    [SerializeField] private float animLength = 0.1f;
    [SerializeField] private Vector2 smallSize; //450, 85
    [SerializeField] private Vector2 bigSize; //465, 100
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightedColor;

    [Header("Buttons")]
    [SerializeField] private Button buttonSkills;
    [SerializeField] private Button buttonStats;
    [SerializeField] private Button buttonOptions;

    [Header("Screens")]
    [SerializeField] private Image screenSkills;
    [SerializeField] private Image screenStats;
    [SerializeField] private Image screenOptions;

    private Dictionary<Button, Image> screens;

    private void Awake()
    {
        screens = new Dictionary<Button, Image>() {
            { buttonSkills, screenSkills},
            { buttonStats, screenStats},
            { buttonOptions, screenOptions},
        };
    }

    private void Start()
    {
        if (!this.gameObject.activeSelf) //shouldnt work but works, leave it like this
        {
            HideMenu();
        }
    }

    public void ShowMenu() {
        this.gameObject.SetActive(true);

        foreach (var item in screens.Keys) { //we need to reset all
            StartCoroutine(UIHelper.ButtonResizeAnim(item, smallSize, 0f, defaultColor)); //anim time 0 coz we are just resetting so we dont need the animation
        }
        foreach (var item in screens.Values) { //because when the menu is set to active, all the screens get set to active
            item.gameObject.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(buttonSkills.gameObject);
        OnButtonSelect(buttonSkills);
    }

    public void HideMenu() {
        this.gameObject.SetActive(false);
    }

    #region Buttons
    public void OnButtonSelect(Button button) {
        StartCoroutine(UIHelper.ButtonResizeAnim(button, bigSize, animLength, highlightedColor));
        screens[button].gameObject.SetActive(true);
    }

    public void OnButtonDeselect(Button button){
        StartCoroutine(UIHelper.ButtonResizeAnim(button, smallSize, animLength, defaultColor));
        screens[button].gameObject.SetActive(false);
    }


    #endregion
}