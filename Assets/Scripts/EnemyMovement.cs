using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private void FixedUpdate()
    {
        transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
    }
}
