using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.ActivateTripleShot();
            }

            PowerUpDrop.StartCooldown();
            Destroy(gameObject);
        }
    }
}
