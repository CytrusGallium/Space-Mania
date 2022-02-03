using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Main;
    
    public bool bossHealthBar = false;    
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        if (!bossHealthBar)
        {
            if (Main == null)
                Main = this;
            else
                Debug.LogWarning("HealthBar singleton is already assigned.");
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}