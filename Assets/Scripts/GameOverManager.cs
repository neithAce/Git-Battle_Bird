using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPopUp;
    public TMP_Text highScoreText;
    
    public void ShowGameOver()
    {
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.SaveHighScore(selectedStage);
        }

        int highScore = PlayerPrefs.GetInt("HighScore_Stage" + selectedStage, 0);
        highScoreText.text = "High Score\n" + highScore;

        gameOverPopUp.SetActive(true);
        Time.timeScale = 0f;
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
