using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    
    private void FixedUpdate()
    {
        transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPosition.x < -0.2f)
        {
            Destroy(gameObject);
        }
    }
}
