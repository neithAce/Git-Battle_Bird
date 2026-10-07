using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public GameObject hitVFX;
    private Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPosition.x < -0.1f || screenPosition.x > 1.1f || screenPosition.y < -0.1f || screenPosition.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            bool damageDealt = playerHealth.TakeDamage(1);
            if(damageDealt && hitVFX != null)
            {
                Instantiate(hitVFX, playerHealth.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
