using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public Image powerUpIcon;
    public TMP_Text timerText;

    private void Start()
    {
        powerUpIcon.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(playerAttack == null)
        {
            return;
        }

        if (playerAttack.IsTripleShotActive())
        {
            powerUpIcon.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            float remainingTime = playerAttack.GetTripleShotRemainingTime();
            timerText.text = remainingTime.ToString("F0");
        }
        else
        {
            powerUpIcon.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
        }
    }
}
