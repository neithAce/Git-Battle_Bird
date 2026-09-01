using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float destroyX = -12f;

    private void FixedUpdate()
    {
        transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
        if(transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }
}
