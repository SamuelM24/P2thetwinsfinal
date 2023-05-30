using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Adjust the maxHealth value to 300 for 30 hits
    public int currentHealth;
    public Slider healthBar;

    // Reference to the RespawnMenu script
    public RespawnMenu respawnMenu;
    public Camera respawnMenuCamera;
    public GameObject respawnMenuUI;

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

        // Call the ShowRespawnMenu method on the RespawnMenu script
        respawnMenu.ShowRespawnMenu();
        EnableMenu();
        respawnMenuCamera.gameObject.SetActive(true);
    }
    private void EnableMenu()
    {
        respawnMenuUI.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10); // Decrease health by 10 when hit by an enemy
        }
    }
}
