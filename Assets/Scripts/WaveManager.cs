using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public float waveStartDuration = 3f;
    private int currentWave = 1;
    public float waveDuration = 90f;
    private float waveTimer;
    private bool spawningActive = true;
    private bool waveTimerFinished = false;
    public EnemySpawner enemySpawner;
    public GameObject stageCompletePopUp;
    private bool stageCompleteShown = false;

    private void Start()
    {
        StartWave(currentWave);
    }

    private void StartWave(int waveNumber)
    {
        currentWave = waveNumber;
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        enemySpawner.SetStageSpawnInterval(selectedStage, waveNumber);
        PowerUpDrop.ResetCooldown();
        waveTimer = waveDuration;
        waveTimerFinished = false;
        spawningActive = false;
        waveText.text = "Wave " + waveNumber;
        waveText.gameObject.SetActive(true);
        Invoke(nameof(BeginWave), waveStartDuration);
    }

    private void BeginWave()
    {
        waveText.gameObject.SetActive(false);
        spawningActive = true;
        Debug.Log("Wave " + currentWave + " dimulai. Spawn enemy diaktifkan.");
        if (enemySpawner != null)
        {
            if(currentWave == 1)
            {
                enemySpawner.SetEnemyPrefab(enemySpawner.enemyPrefab);
            }
            else if(currentWave == 2)
            {
                enemySpawner.SetEnemyPrefab(enemySpawner.enemyShooterPrefab);
                enemySpawner.StartPowerUpSpawning();
            }
            else if(currentWave == 3)
            {
                enemySpawner.SetEnemyPrefab(enemySpawner.enemyChaserPrefab);
                enemySpawner.StartPowerUpSpawning();
            }
            else if(currentWave == 4)
            {
                enemySpawner.SetMixedEnemySpawn();
                enemySpawner.StartPowerUpSpawning();
            }

            enemySpawner.StartSpawning();
        }
        else
        {
            Debug.LogError("EnemySpawner reference is not set in WaveManager.");
        }
    }

    private void HideWaveText()
    {
        waveText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (spawningActive)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0f)
            {
                waveTimer = 0f;
                spawningActive = false;
                waveTimerFinished = true;
                if (enemySpawner != null)
                {
                    enemySpawner.StopSpawning();
                }
                Debug.Log("Waktu wave habis. Spawn enemy dihentikan.");
            }
        }
        
        if(waveTimerFinished && !AreWaveEnemiesAlive() && !stageCompleteShown)
        {
            StartNextWave();
        }
    }

    private void CheckBattleEnemies()
    {
        if (AreBattleEnemiesAlive())
        {
            Debug.Log("Battle enemies are still alive.");
        }
        else
        {
            Debug.Log("All battle enemies defeated. ");
        }
    }

    private bool AreBattleEnemiesAlive()
    {
        GameObject[] battleEnemies = GameObject.FindGameObjectsWithTag("BattleEnemy");
        return battleEnemies.Length > 0;
    }

    private bool AreWaveEnemiesAlive()
    {
        WaveEnemy[] waveEnemies = FindObjectsOfType<WaveEnemy>();
        return waveEnemies.Length > 0;
    }

    private void StartNextWave()
    {
        Debug.Log("StartNextWave dipanggil. Current Wave: " + currentWave);
        currentWave++;
        if(currentWave <= 4)
        {
            StartWave(currentWave);
        }
        else
        {
            stageCompleteShown = true;
            enemySpawner.StopPowerUpSpawning();
            StageCompleteManager stageCompleteManager = stageCompletePopUp.GetComponent<StageCompleteManager>();
            if(stageCompleteManager != null)
            {
                stageCompleteManager.ShowStageComplete();
            }
        }
    }
}
