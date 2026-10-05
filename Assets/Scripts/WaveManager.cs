using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public float waveStartDuration = 3f;
    private int currentWave = 1;
    public float waveDuration = 90f;
    private float waveTimer;
    private float battleTimer;
    private bool battleEnemySpawned = false;
    private bool battleShooterSpawned = false;
    private bool secondBattleEnemySpawned = false;
    private bool secondBattleShooterSpawned = false;
    private bool thirdBattleEnemySpawned = false;
    private bool spawningActive = true;
    private bool waveTimerFinished = false;
    public EnemySpawner enemySpawner;
    public BattleEnemySpawner battleEnemySpawner;
    public CloudSpawner cloudSpawner;
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
        battleTimer = 0f;
        battleEnemySpawned = false;
        battleShooterSpawned = false;
        secondBattleEnemySpawned = false;
        secondBattleShooterSpawned = false;
        thirdBattleEnemySpawned = false;
        if(cloudSpawner != null)
        {
            cloudSpawner.StartCloudSpawning();
        }

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
            int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
            if(selectedStage == 2 || selectedStage == 3)
            {
                enemySpawner.SetMixedEnemySpawn();
                enemySpawner.StartPowerUpSpawning();
            }
            else if(currentWave == 1)
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
            battleTimer += Time.deltaTime;
            //stage 2
            if(battleTimer >= 80f && !battleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if(selectedStage == 2 && currentWave == 1)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawned = true;
                }
            }

            if(battleTimer >= 70f && !battleShooterSpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if(selectedStage == 2 && currentWave == 2)
                {
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleShooterSpawned = true;
                }
            }

            if(battleTimer >= 60f && !battleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if(selectedStage == 2 && currentWave == 3)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawned = true;
                }
            }

            if(battleTimer >= 80f && !battleShooterSpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if(selectedStage == 2 && currentWave == 3)
                {
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleShooterSpawned = true;
                }
            }

            if(battleTimer >= 30f && !battleEnemySpawned && !battleShooterSpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 2 && currentWave == 4)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleEnemySpawned = true;
                    battleShooterSpawned = true;
                }
            }

            if(battleTimer >= 60f && !secondBattleEnemySpawned && !secondBattleShooterSpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if(selectedStage == 2 && currentWave == 4)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    secondBattleEnemySpawned = true;
                    secondBattleShooterSpawned = true;
                }
            }
            //stage 3
            if (battleTimer >= 45f && !secondBattleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 3 && currentWave == 1)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleEnemySpawned = true;
                    secondBattleEnemySpawned = true;
                    battleShooterSpawned = true;
                }
            }

            if (battleTimer >= 30f && !battleShooterSpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 3 && currentWave == 2)
                {
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleShooterSpawned = true;
                }
            }

            if(battleTimer >= 70f && !secondBattleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if(selectedStage == 3 && currentWave == 2)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    secondBattleEnemySpawned = true;
                    secondBattleShooterSpawned = true;
                }
            }

            if(battleTimer >= 1f && !battleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 3 && currentWave == 3)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawned = true;
                }
            }

            if (battleTimer >= 2f && !secondBattleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 3 && currentWave == 3)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    secondBattleEnemySpawned = true;
                }
            }

            if (battleTimer >= 3f && !thirdBattleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 3 && currentWave == 3)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    thirdBattleEnemySpawned = true;
                }
            }

            if(battleTimer >= 20f && !battleShooterSpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if(selectedStage == 3 && currentWave == 3)
                {
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleShooterSpawned = true;
                }
            }

            if(battleTimer >= 20f && !battleEnemySpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 3 && currentWave == 4)
                {
                    battleEnemySpawner.SpawnBattleEnemy();
                    battleEnemySpawned = true;
                }
            }

            if(battleTimer >= 40f && !battleShooterSpawned)
            {
                int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
                if (selectedStage == 3 && currentWave == 4)
                {
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleEnemySpawner.SpawnBattleEnemyShooter();
                    battleShooterSpawned = true;
                }
            }
            if (waveTimer <= 0f)
            {
                waveTimer = 0f;
                spawningActive = false;
                waveTimerFinished = true;
                if (enemySpawner != null)
                {
                    enemySpawner.StopSpawning();
                }
                if (cloudSpawner != null)
                {
                    cloudSpawner.StopCloudSpawning();
                }
                Debug.Log("Waktu wave habis. Spawn enemy dihentikan.");
            }
        }

        if (waveTimerFinished && !AreWaveEnemiesAlive() && !stageCompleteShown)
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
