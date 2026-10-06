using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StageCompleteManager : MonoBehaviour
{
    public GameObject stageCompletePopUp;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public void ShowStageComplete()
    {
        int selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.SaveHighScore(selectedStage);
            scoreText.text = "SCORE\n" + scoreManager.currentScore.ToString();
        }

        int highScore = PlayerPrefs.GetInt("HighScore_Stage" + selectedStage, 0);
        highScoreText.text = "HIGHSCORE\n" + highScore.ToString();
        stageCompletePopUp.SetActive(true);
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayStageCompleteMusic();
        }

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

    public void NextStage()
    {
        Time.timeScale = 1f;
        int currentStage = PlayerPrefs.GetInt("SelectedStage", 1);
        int nextStage = currentStage + 1;
        PlayerPrefs.SetInt("SelectedStage", nextStage);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}
