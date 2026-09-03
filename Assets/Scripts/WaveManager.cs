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

    private void Start()
    {
        StartWave(currentWave);
        InvokeRepeating(nameof(CheckBattleEnemies), 4f, 1f);
    }

    private void StartWave(int waveNumber)
    {
        currentWave = waveNumber;
        waveTimer = waveDuration;
        spawningActive = false;
        waveText.text = "Wave " + waveNumber;
        waveText.gameObject.SetActive(true);
        Invoke(nameof(BeginWave), waveStartDuration);
    }

    private void BeginWave()
    {
        waveText.gameObject.SetActive(false);
        spawningActive = true;
    }

    private void HideWaveText()
    {
        waveText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!spawningActive)
        {
            return;
        }

        waveTimer -= Time.deltaTime;
        if(waveTimer <= 0f)
        {
            waveTimer = 0f;
            spawningActive = false;
            Debug.Log("Waktu wave habis. Spawn enemy dihentikan.");
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
}
