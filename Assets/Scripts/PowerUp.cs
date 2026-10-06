using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip pickupSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.ActivateTripleShot();
            }

            if(AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(pickupSFX);
            }

            PowerUpDrop.StartCooldown();
            Destroy(gameObject);
        }
    }
}
