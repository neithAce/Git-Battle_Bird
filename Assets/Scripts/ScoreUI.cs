using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public ScoreManager scoreManager;

    private void Update()
    {
        scoreText.text = "Score: " + scoreManager.currentScore.ToString("D10");
    }
}
