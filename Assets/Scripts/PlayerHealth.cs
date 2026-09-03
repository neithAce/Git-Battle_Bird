using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public TMP_Text healthText;
    public GameOverManager gameOverManager;
    private bool isDead = false;
    private float immunityTimer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (immunityTimer > 0f)
        {
            immunityTimer -= Time.deltaTime;
        }
    }

    public bool TakeDamage(int damage)
    {
        if (isDead)
        {
            return false;
        }

        if(immunityTimer > 0f) 
        {
            return false;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        immunityTimer = 2f;
        UpdateHealthUI();
        Debug.Log("Player took damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            gameOverManager.ShowGameOver();
        }

        return true;
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString();
    }
}
