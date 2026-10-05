using UnityEngine;

public class PowerUpRotation : MonoBehaviour
{
    public float rotationSpeed = 180f;
    private float rotationAngle = 0f;

    private void Update()
    {
        rotationAngle += rotationSpeed * Time.deltaTime;
        float scaleX = Mathf.Cos(rotationAngle * Mathf.Deg2Rad);
        transform.localScale = new Vector3(Mathf.Abs(scaleX), 1f, 1f);
    }
}
