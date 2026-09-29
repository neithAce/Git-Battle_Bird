using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float spawnInterval = 12f;
    public float minY = 1.5f;
    public float maxY = 4f;
    private bool spawning = false;

    public void StartCloudSpawning()
    {
        if (spawning)
        {
            return;
        }

        spawning = true;
        InvokeRepeating(nameof(SpawnCloud), 0f, spawnInterval);
    }

    public void StopCloudSpawning()
    {
        spawning = false;
        CancelInvoke(nameof(SpawnCloud));
    }

    private void SpawnCloud()
    {
        if (!spawning)
        {
            return;
        }

        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(rightEdge + 2f, randomY, 0f);
        Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
    }
}
