using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyShooterPrefab;
    public GameObject enemyPowerUpPrefab;
    public GameObject enemyChaserPrefab;
    public float spawnInterval = 3f;
    public float wave4SpawnInterval = 2.5f;
    public float stage2And3StartInterval = 2.5f;
    public float stage2And3IntervalDecrease = 0.2f;
    public float stage3Wave4SpawnInterval = 6f;
    public float minY = -4f;
    public float maxY = 4f;
    [Range(0f, 1f)] public float shooterSpawnChance = 0.3f;
    private bool spawning = false;
    private GameObject currentEnemyPrefab;
    private bool mixedSpawn = false;
    private bool powerUpSpawning = false;
    private bool doubleSpawn = false;

    public void StartSpawning()
    {
        if (spawning)
        {
            Debug.Log("Spawning is already active.");
            return;
        }

        spawning = true;
        float intervalToUse = spawnInterval;
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        if(selectedStage == 1 && mixedSpawn)
        {
            intervalToUse = wave4SpawnInterval;
        }

        Debug.Log("EnemySpawner: StartSpawning dipanggil. Interval: " + intervalToUse);
        InvokeRepeating(nameof(SpawnEnemy), 1f, intervalToUse);
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
    }

    public void SetStageSpawnInterval(int stageNumber, int waveNumber)
    {
        doubleSpawn = (stageNumber == 3 && waveNumber == 4);
        if(stageNumber == 3 && waveNumber == 4)
        {
            spawnInterval = stage3Wave4SpawnInterval;
        }
        else if(stageNumber == 2 || stageNumber == 3)
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
            SpawnMixedEnemy(spawnPosition);
            if (doubleSpawn)
            {
                float secondRandomY = Random.Range(minY, maxY);
                Vector3 secondSpawnPosition = new Vector3(rightEdge + 1f, secondRandomY, 0f);
                SpawnMixedEnemy(secondSpawnPosition);
            }
        }
        else if (currentEnemyPrefab != null)
        {
            Instantiate(currentEnemyPrefab, spawnPosition, Quaternion.identity);
            if (doubleSpawn)
            {
                Instantiate(currentEnemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private void SpawnMixedEnemy(Vector3 spawnPosition)
    {
        float randomValue = Random.value;
        if(randomValue < 0.25f)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else if(randomValue < 0.50f)
        {
            Instantiate(enemyShooterPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyChaserPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
