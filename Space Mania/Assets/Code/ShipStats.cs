using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds information about the ship that we can pass between scenes like health, shield and upgrades.
public class ShipStats : MonoBehaviour
{
    public int startingHealth = 100;
    public int startingShield = 50;
    public int startingMaxHealth = 100;
    public int startingMaxShield = 100;
    
    private static ShipStats singleton;
    private static bool isInitialized = false;
    
    private static int currentHealth = 0;
    private static int currentShield = 0;

    public static ShipStats Main { get { return singleton; } }

    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Debug.LogWarning("Ship Stats singleton not NULL.");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isInitialized)
        {
            print("Initializing ship stats...");
            currentHealth = startingHealth;
            currentShield = startingShield;
            isInitialized = true;
        }
    }

    public static void SaveStats()
    {
        currentHealth = singleton.GetComponent<PlayerHealthAndShield>().currentHealth;
        currentShield = singleton.GetComponent<PlayerHealthAndShield>().currentShield;
        print("Ship stats saved...");
    }

    public static int GetHealth()
    {
        return currentHealth;
    }

    public static int GetShield()
    {
        return currentShield;
    }
}