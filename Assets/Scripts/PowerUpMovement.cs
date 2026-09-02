using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private void FixedUpdate()
    {
        transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPosition.x < -0.1f)
        {
            Destroy(gameObject);
        }
    }
}
