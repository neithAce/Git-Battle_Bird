using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject stageCompletePopUp;
    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameOverPanel != null && gameOverPanel.activeSelf)
            {
                return;
            }

            if(stageCompletePopUp != null && stageCompletePopUp.activeSelf)
            {
                return;
            }

            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPauseMusic();
        }

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.StopSpecialMusic();
            AudioManager.Instance.PlayMusic();
        }

        Time.timeScale = 1f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
