using UnityEngine;
using TMPro;

public class StageSelectionManager : MonoBehaviour
{
    public GameObject stagePopUp;
    public TMP_Text highScoreText;
    private int selectedStage;

    public void OpenStage1()
    {
        selectedStage = 1;
        ShowPopUp();
    }

    public void OpenStage2()
    {
        selectedStage = 2;
        ShowPopUp();
    }

    public void OpenStage3()
    {
        selectedStage = 3;
        ShowPopUp();
    }

    private void ShowPopUp()
    {
        stagePopUp.SetActive(true);
        int highScore = PlayerPrefs.GetInt("HighScore_Stage" + selectedStage, 0);
        highScoreText.text = "High Score\n" + highScore;
    }

    public void ClosePopUp()
    {
        stagePopUp.SetActive(false);
    }
}
