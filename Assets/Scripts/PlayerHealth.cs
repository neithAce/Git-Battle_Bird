using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public TMP_Text healthText;
    public GameOverManager gameOverManager;
    public CameraShake cameraShake;
    public SpriteRenderer playerVisual;
    public Color damageFlashColor = new Color(1f, 0.9f, 0.5f, 1f);
    public float blinkInterval = 0.1f;
    public float transparentAlpha = 0.25f;
    private bool isDead = false;
    private float immunityTimer = 0f;
    private Coroutine damageBlinkCoroutine;
    public AudioClip damageSFX;

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
        if(damageBlinkCoroutine != null)
        {
            StopCoroutine(damageBlinkCoroutine);
        }

        damageBlinkCoroutine = StartCoroutine(DamageBlink());
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(damageSFX);
        }

        if(cameraShake != null)
        {
            cameraShake.Shake();
        }

        UpdateHealthUI();
        Debug.Log("Player took damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            cameraShake.StopShake();
            gameOverManager.ShowGameOver();
        }

        return true;
    }

    private IEnumerator DamageBlink()
    {
        Color originalColor = playerVisual.color;
        float elapsed = 0f;
        bool visible = false;
        while (elapsed < 2f)
        {
            visible = !visible;
            Color blinkColor = damageFlashColor;
            if(visible)
            {
                blinkColor.a = 1f;
            }
            else
            {
                blinkColor.a = transparentAlpha;
            }

            playerVisual.color = blinkColor;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        playerVisual.color = originalColor;
        damageBlinkCoroutine = null;
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString();
    }
}
