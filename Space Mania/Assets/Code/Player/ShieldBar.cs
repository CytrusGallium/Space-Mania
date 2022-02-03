using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    private static ShieldBar singleton;
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Debug.LogWarning("Shield Bar singleton not NULL.");
    }

    void Start()
    {
        SetMaxShield(ShipStats.Main.startingMaxShield);
        SetShield(ShipStats.Main.startingShield);
    }

    public static void SetMaxShield(int shield)
    {
        singleton.slider.maxValue = shield;
        singleton.slider.value = shield;

        singleton.fill.color = singleton.gradient.Evaluate(1f);
    }

    public static void SetShield(int shield)
    {
        singleton.slider.value = shield;
        singleton.fill.color = singleton.gradient.Evaluate(singleton.slider.normalizedValue);
    }
}