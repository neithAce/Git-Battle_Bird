using UnityEngine;

public class PowerUpDrop : MonoBehaviour
{
    public GameObject powerUpPrefab;
    private static float nextDroptime = 0f;

    public void DropPowerUp()
    {
        if(powerUpPrefab == null)
        {
            return;
        }

        if(Time.time < nextDroptime)
        {
            return;
        }

        Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
    }

    public static void StartCooldown()
    {
        nextDroptime = Time.time + 30f;
    }

    public static void ResetCooldown()
    {
        nextDroptime = 0f;
    }
}
