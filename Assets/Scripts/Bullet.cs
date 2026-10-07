using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    public AudioClip hitSFX;
    public GameObject hitVFX;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.fixedDeltaTime;
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        if (transform.position.x > rightEdge + 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            if(hitVFX != null)
            {
                Instantiate(hitVFX, transform.position, Quaternion.identity);
            }

            enemy.TakeDamage(1);
            if(AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(hitSFX);
            }

            Destroy(gameObject);
        }
    }
}
