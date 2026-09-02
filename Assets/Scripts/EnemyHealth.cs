using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Score score = GetComponent<Score>();
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (score != null && scoreManager != null)
            {
                scoreManager.AddScore(score.scoreValue);
            }

            PowerUpDrop powerUpDrop = GetComponent<PowerUpDrop>();
            if (powerUpDrop != null)
            {
                powerUpDrop.DropPowerUp();
            }

            Destroy(gameObject);
        }
    }
}
