using UnityEngine;

public class BattleEnemySpawner : MonoBehaviour
{
    public GameObject battleEnemyPrefab;
    public GameObject battleEnemyShooterPrefab;
    public float minY = -4f;
    public float maxY = 4f;

    [ContextMenu("Spawn Battle Enemy")]
    public void SpawnBattleEnemy()
    {
        float randomY = Random.Range(minY, maxY);
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        Instantiate(battleEnemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnBattleEnemyShooter()
    {
        float randomY = Random.Range(minY, maxY);
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        Vector3 spawnPosition = new Vector3(rightEdge + 1f, randomY, 0f);
        Instantiate(battleEnemyShooterPrefab, spawnPosition, Quaternion.identity);
    }
}
