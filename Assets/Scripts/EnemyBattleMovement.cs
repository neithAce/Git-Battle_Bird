using UnityEngine;

public class EnemyBattleMovement : MonoBehaviour
{
    public float horizontalRange = 2f;
    public float verticalRange = 1.5f;
    public float moveSpeed = 2f;
    public float enterSpeed = 5f;
    public float targetViewportX = 0.7f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float time;
    private bool entering = true;

    private void Start()
    {
        startPosition = transform.position;
        float targetX = Camera.main.ViewportToWorldPoint(new Vector3(targetViewportX, 0f, 0f)).x;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (entering)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enterSpeed * Time.fixedDeltaTime);
            if(Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                startPosition = transform.position;
                time = 0f;
                entering = false;
            }

            return;
        }

        time += moveSpeed * Time.fixedDeltaTime;
        float x = Mathf.Sin(time) * horizontalRange;
        float y = Mathf.Sin(time * 2f) * verticalRange;
        transform.position = startPosition + new Vector3(x, y, 0f);
    }
}
