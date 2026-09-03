using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyShooterPrefab;
    public GameObject enemyPowerUpPrefab;
    public GameObject enemyChaserPrefab;
    public GameObject enemyBattlePrefab;
    public GameObject enemyBattleShooterPrefab;
    public float spawnInterval = 2f;
    public float minY = -4f;
    public float maxY = 4f;
    [Range(0f, 1f)] public float shooterSpawnChance = 0.3f;
    private bool spawning = false;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (spawning)
        {
            return;
        }

        spawning = true;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    public void StopSpawning()
    {
        spawning = false;
        CancelInvoke(nameof(SpawnEnemy));
    }

    private void SpawnEnemy()
    {
        if (!spawning)
        {
            return;
        }

        float randomY = Random.Range(minY, maxY);
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        GameObject enemyToSpawn;
        if(Random.value < 0.25f)
        {
            enemyToSpawn = enemyChaserPrefab;
        }
        else if(Random.value < 0.55f)
        {
            enemyToSpawn = enemyShooterPrefab;
        }
        else if(Random.value < 0.65f)
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
