using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    private FloatingHealthBar healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealthBar();

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(health, 100f);
        }
    }
}
