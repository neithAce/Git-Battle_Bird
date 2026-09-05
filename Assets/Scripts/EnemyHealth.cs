using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public bool scaleWithStage = true;
    private int currentHealth;

    private void Start()
    {
        if (!scaleWithStage)
        {
            currentHealth = maxHealth;
            return;
        }

        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        if(selectedStage == 2)
        {
            currentHealth = maxHealth + 1;
        }
        else if(selectedStage == 3)
        {
            currentHealth = maxHealth + 2;
        }
        else
        {
            currentHealth = maxHealth;
        }
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
