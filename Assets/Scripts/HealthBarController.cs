using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider slider;   // health bar

    
    public void UpdateHealth(float currentHealth,float maxHealth)    // updating health bar
    {
        slider.value = currentHealth/maxHealth;
    }
}
