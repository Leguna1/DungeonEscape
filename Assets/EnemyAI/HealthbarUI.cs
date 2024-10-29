using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthbarUI : MonoBehaviour
{
    
    Health health;
    public float maxHealth;
    public float currentHealth;
    public Slider healthSlider;

    void Start()
    {
        maxHealth = health.maxHealth;
        currentHealth = health.maxHealth;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }
    }
}
