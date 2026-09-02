using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int selectedStage;

    void Start()
    {
        selectedStage = PlayerPrefs.GetInt("SelectedStage", 1);
        if (selectedStage == 1)
        {
            Debug.Log("Stage 1 - Easy");
        }
        else if (selectedStage == 2)
        {
            Debug.Log("Stage 2 - Normal");
        }
        else if (selectedStage == 3)
        {
            Debug.Log("Stage 3 - Hard");
        }
    }
}
