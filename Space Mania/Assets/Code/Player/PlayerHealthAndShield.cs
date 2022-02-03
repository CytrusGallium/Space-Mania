using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAndShield : MonoBehaviour
{
    [SerializeField] private ColoredFlash flashEffect;
    [SerializeField] private Color[] colors;

    private static PlayerHealthAndShield singleton;

    public static int CurrentShield {  get {return singleton.currentShield; } }

    public int maxHealth = 100;
    public int maxShield = 100;
    public int currentHealth = 100;
    public int currentShield = 50;

    public Animator animator;

    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Debug.LogWarning("Player Health singleton not NULL.");
    }

    void Start()
    {
        currentHealth = ShipStats.GetHealth();
        currentShield = ShipStats.GetShield();

        HealthBar.Main.SetMaxHealth(maxHealth);
        HealthBar.Main.SetHealth(currentHealth);

        ShieldBar.SetMaxShield(maxShield);
        ShieldBar.SetShield(currentShield);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        
        if (collision.CompareTag("Enime") || collision.CompareTag("Spider Boss"))
        {
            audioManager.Play("PlayerHit");
            TakeDamage(10);
        }

        if (collision.CompareTag("Ground"))
        {
            audioManager.Play("GroundHit");
            TakeDamage(10);
        }

        if (collision.CompareTag("Spike"))
        {
            audioManager.Play("GroundHit");
            TakeDamage(1000);
        }

        if (collision.CompareTag("Healer"))
        {
            audioManager.Play("Heal");
            Healing(35);
        }
    }

    public void TakeDamage(int damage, bool ignoreShield = false)
    {
        Color red = colors[0];
        flashEffect.Flash(red);

        if (Shield.IsShielded())
        {
            int nonAbsorbedDamage = ApplyDamageToShield(damage);
            currentHealth -= nonAbsorbedDamage;
        }
        else
        {
            currentHealth -= damage;
        }

        HealthBar.Main.SetHealth(currentHealth);
        ShieldBar.SetShield(currentShield);

        if (currentHealth <= 0)
        {
            StartCoroutine("Die");
        }
    }

    // Returns how much damage was not absorbed by the shield.
    private int ApplyDamageToShield(int damage)
    {
        if (currentShield < 0) // Make sure the miniumum is zero
            currentShield = 0;
        
        if (currentShield == 0) // Cannot absorb damage
        {
            return damage;
        }
        else if (currentShield >= damage) // Shield have absorbed all damage
        {
            currentShield -= damage;
            return 0;
        }
        else // Shield can absord only a portion of the damage
        {
            int absorbed = currentShield;
            currentShield = 0;

            return damage - absorbed; // Return what was not absorbed
        }
    }

    public void Healing(int Heal)
    {
        Color green = colors[1];
        flashEffect.Flash(green);
        
        // Heal.
        currentHealth += Heal;

        // Prevent over healing.
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        HealthBar.Main.SetHealth(currentHealth);
    }

    IEnumerator Die()
    {
        GetComponent<PlayerMovement>().shipAlive = false;

        Music.StopMusic();
        
        animator.Play("Death Effect");

        FindObjectOfType<AudioManager>().Play("PlayerDie");

        FindObjectOfType<GameManager>().EndGame();

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}