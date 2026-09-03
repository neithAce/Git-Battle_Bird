using UnityEngine;

public class EnemyChaserDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            bool damageDealt = playerHealth.TakeDamage(damage);
            if (damageDealt)
            {
                Destroy(gameObject);
            }
        }
    }
}
