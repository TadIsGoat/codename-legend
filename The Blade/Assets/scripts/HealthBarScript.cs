using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private Image healthbar;

    void Update()
    {
        healthbar.fillAmount = healthScript.currentHealth / 100;
    }
}