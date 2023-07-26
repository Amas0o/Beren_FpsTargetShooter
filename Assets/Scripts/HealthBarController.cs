using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider slider;

    
    public void UpdateHealth(float currentHealth,float maxHealth)
    {
        slider.value = currentHealth/maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
