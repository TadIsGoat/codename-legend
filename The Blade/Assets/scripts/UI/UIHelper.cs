using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class UIHelper
{
    public static IEnumerator ButtonResizeAnim/*Contains color reset too*/(Button button, Vector2 targetSize, float resizeTime, Color color) {
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
}
