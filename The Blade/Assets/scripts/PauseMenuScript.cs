using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    /*

    Default button color: FAFBF6 (250, 251, 246)
    Highlighted button color: C6B7BE (198, 183, 190)

    */

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

    void Awake()
    {
        this.gameObject.SetActive(false);
        screens = new Dictionary<Button, Image>() {
            { buttonSkills, screenSkills},
            { buttonStats, screenStats},
            { buttonOptions, screenOptions},
        };
    }

    public void ShowMenu() {
        this.gameObject.SetActive(true);

        foreach (var item in screens.Keys) { //we need to reset all
            StartCoroutine(ButtonResizeAnim(item, smallSize, 0f, defaultColor)); //anim time 0 coz we are just resetting so we dont need the animation
        }
        foreach (var item in screens.Values) { //because when the menu is set to active, all the screens get set to active
            item.gameObject.SetActive(false);
        }
        OnButtonSelect(buttonSkills);
    }

    public void HideMenu() {
        this.gameObject.SetActive(false);
    }

    #region Buttons
    public void OnButtonSelect(Button button) {
        StartCoroutine(ButtonResizeAnim(button, bigSize, animLength, highlightedColor));
        screens[button].gameObject.SetActive(true);
    }

    public void OnButtonDeselect(Button button){
        StartCoroutine(ButtonResizeAnim(button, smallSize, animLength, defaultColor));
        screens[button].gameObject.SetActive(false);
    }

    IEnumerator ButtonResizeAnim/*Contains color reset too*/(Button button, Vector2 targetSize, float resizeTime, Color color) {
        RectTransform rect = button.GetComponent<RectTransform>();
        Vector2 originalSize = rect.sizeDelta;
        float elapsed = 0f;

        button.image.color = color;
        
        while (elapsed < resizeTime) 
        {
            elapsed += Time.deltaTime;
            rect.sizeDelta = Vector2.Lerp(originalSize, targetSize, elapsed / resizeTime);
            yield return null;
        }
        rect.sizeDelta = targetSize;
    }
    #endregion
}