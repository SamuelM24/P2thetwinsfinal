using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;

    // Reference to the RespawnMenu script
    public RespawnMenu respawnMenu;
    public GameObject[] enemies;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.value = healthPercentage;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    private void Die()
    {
        // Game over logic or player death effects here
        Debug.Log("Player died");
        Destroy(gameObject);

        // Disable enemy GameObjects
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }

        // Pause the game, except for the respawn menu
        Time.timeScale = 0f;
        respawnMenu.ShowRespawnMenu();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}
