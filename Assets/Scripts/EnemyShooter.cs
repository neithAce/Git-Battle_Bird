using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float shootInterval = 3f;
    public Transform firePoint;
    private Transform player;
    public AudioClip shootSFX;
    public GameObject enemyBulletFireVFX;

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

        Transform shootPoint = firePoint != null ? firePoint : transform;
        Vector3 direction = (player.position - shootPoint.position).normalized;
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(shootSFX);
        }

        if(enemyBulletFireVFX != null)
        {
            Instantiate(enemyBulletFireVFX, shootPoint.position, Quaternion.identity);
        }

        GameObject bullet = Instantiate(enemyBulletPrefab, shootPoint.position, Quaternion.identity);
        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        if(enemyBullet != null)
        {
            enemyBullet.SetDirection(direction);
        }
    }
}
