using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public void SaveHighScore(int stageNumber)
    {
        string key = "HighScore_Stage" + stageNumber;
        int oldHighScore = PlayerPrefs.GetInt(key, 0);
        if (currentScore > oldHighScore)
        {
            PlayerPrefs.SetInt(key, currentScore);
            PlayerPrefs.Save();
        }
    }
}
