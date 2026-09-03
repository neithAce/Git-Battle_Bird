using UnityEngine;

public class EnemyBattleMovement : MonoBehaviour
{
    public float horizontalRange = 2f;
    public float verticalRange = 1.5f;
    public float moveSpeed = 2f;
    private Vector3 startPosition;
    private float time;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        time += moveSpeed * Time.fixedDeltaTime;
        float x = Mathf.Sin(time) * horizontalRange;
        float y = Mathf.Sin(time * 2f) * verticalRange;
        transform.position = startPosition + new Vector3(x, y, 0f);
    }
}
