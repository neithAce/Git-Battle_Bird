using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyShooterPrefab;
    public GameObject enemyPowerUpPrefab;
    public float spawnInterval = 2f;
    public float minY = -4f;
    public float maxY = 4f;
    [Range(0f, 1f)] public float shooterSpawnChance = 0.3f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        float randomY = Random.Range(minY, maxY);
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        GameObject enemyToSpawn;
        if(Random.value < 0.3f)
        {
            enemyToSpawn = enemyShooterPrefab;
        }
        else if(Random.value < 0.4f)
        {
            enemyToSpawn = enemyPowerUpPrefab;
        }
        else
        {
            enemyToSpawn = enemyPrefab;
        }

        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }
}
