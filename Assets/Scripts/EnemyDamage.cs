using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            bool damageDealt = playerHealth.TakeDamage(damage);
            if(damageDealt && hitVFX != null)
            {
                Instantiate(hitVFX, playerHealth.transform.position, Quaternion.identity);
            }
        }
    }
}
