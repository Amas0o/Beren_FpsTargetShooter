using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider slider;   // slider for health bar display

    
    public void UpdateHealth(float currentHealth,float maxHealth)    // updates health bar to percentage current health
    {
        slider.value = currentHealth/maxHealth;
    }
}
