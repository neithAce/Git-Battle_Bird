using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float shootInterval = 3f;
    private Transform player;
    public AudioClip shootSFX;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        InvokeRepeating(nameof(Shoot), 2f, shootInterval);
    }

    private void Shoot()
    {
        if(player == null)
        {
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(shootSFX);
        }

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        if(enemyBullet != null)
        {
            enemyBullet.SetDirection(direction);
        }
    }
}
