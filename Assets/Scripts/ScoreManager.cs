using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;

    public void AddScore(int amount)
    {
        currentScore += amount;
    }
}
