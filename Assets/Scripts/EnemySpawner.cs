using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyShooterPrefab;
    public GameObject enemyPowerUpPrefab;
    public GameObject enemyChaserPrefab;
    public GameObject enemyBattlePrefab;
    public GameObject enemyBattleShooterPrefab;
    public float spawnInterval = 3f;
    public float wave4SpawnInterval = 2.5f;
    public float stage2And3StartInterval = 2.5f;
    public float stage2And3IntervalDecrease = 0.2f;
    public float minY = -4f;
    public float maxY = 4f;
    [Range(0f, 1f)] public float shooterSpawnChance = 0.3f;
    private bool spawning = false;
    private GameObject currentEnemyPrefab;
    private bool mixedSpawn = false;
    private bool powerUpSpawning = false;

    public void StartSpawning()
    {
        if (spawning)
        {
            Debug.Log("Spawning is already active.");
            return;
        }

        spawning = true;
        Debug.Log("EnemySpawner: StartSpawning dipanggil. Interval: " + spawnInterval);
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    public void SetEnemyPrefab(GameObject enemyPrefabToUse)
    {
        currentEnemyPrefab = enemyPrefabToUse;
        mixedSpawn = false;
    }

    public void SetMixedEnemySpawn()
    {
        currentEnemyPrefab = null;
        mixedSpawn = true;
        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 1f, wave4SpawnInterval);
    }

    public void SetStageSpawnInterval(int stageNumber, int waveNumber)
    {
        if(stageNumber == 2 || stageNumber == 3)
        {
            float newInterval = stage2And3StartInterval - (waveNumber - 1) * stage2And3IntervalDecrease;
            spawnInterval = newInterval;
        }
    }

    public void SpawnPowerUpEnemy()
    {
        float randomY = Random.Range(minY, maxY);
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        Instantiate(enemyPowerUpPrefab, spawnPosition, Quaternion.identity);
    }

    public void StartPowerUpSpawning()
    {
        if (powerUpSpawning)
        {
            return;
        }

        powerUpSpawning = true;
        InvokeRepeating(nameof(SpawnPowerUpEnemy), 1f, 10f);
    }

    public void StopPowerUpSpawning()
    {
        powerUpSpawning = false;
        CancelInvoke(nameof(SpawnPowerUpEnemy));
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
        if (mixedSpawn)
        {
            float randomValue = Random.value;
            if (randomValue < 0.25f)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            else if (randomValue < 0.50f)
            {
                Instantiate(enemyShooterPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyChaserPrefab, spawnPosition, Quaternion.identity);
            }
        }
        else if (currentEnemyPrefab != null)
        {
            Instantiate(currentEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
