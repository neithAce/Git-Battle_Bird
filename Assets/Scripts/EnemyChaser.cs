using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float exitSpeed = 10f;
    public float circleRadius = 0.5f;
    public float circleSpeed = 360f;
    public int rotateCount = 3;
    public float exitRightDistance = 3f;
    public float exitVerticalDistance = 3f;
    private Transform player;
    private Vector3 targetPosition;
    private Vector3 moveDirection;
    private Vector3 circleCenter;
    private Vector3 circleStartOffSet;
    private float circleAngle = 0f;
    private int completedRotations = 0;

    private enum ChaserState
    {
        Charging,
        Rotating,
        Exiting
    }

    private ChaserState currentState;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject != null)
        {
            player = playerObject.transform;
            setTarget();
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        if(currentState == ChaserState.Charging)
        {
            ChangeToTarget();
        }
        else if(currentState == ChaserState.Rotating)
        {
            RotateAroundCenter();
        }
        else if(currentState == ChaserState.Exiting)
        {
            ExitArena();
        }
    }

    private void setTarget()
    {
        targetPosition = player.position;
        moveDirection = (targetPosition - transform.position).normalized;
        currentState = ChaserState.Charging;
    }

    private void ChangeToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        if(Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            StartRotating();
        }
    }

    private void StartRotating()
    {
        completedRotations = 0;
        circleAngle = 0f;
        float verticalDirection = Random.value < 0.5f ? 1f : -1f;
        circleCenter = transform.position + Vector3.up * verticalDirection * circleRadius;
        circleStartOffSet = transform.position - circleCenter;
        currentState = ChaserState.Rotating;
    }

    private void RotateAroundCenter()
    {
        circleAngle += circleSpeed * Time.fixedDeltaTime;
        float angleInRadians = circleAngle * Mathf.Deg2Rad;
        float rotatedX = circleStartOffSet.x * Mathf.Cos(angleInRadians) - circleStartOffSet.y * Mathf.Sin(angleInRadians);
        float rotatedY = circleStartOffSet.x * Mathf.Sin(angleInRadians) + circleStartOffSet.y * Mathf.Cos(angleInRadians);
        transform.position = circleCenter + new Vector3(rotatedX, rotatedY, 0f);
        if(circleAngle >= 360f)
        {
            circleAngle -= 360f;
            completedRotations++;
            if(completedRotations >= rotateCount)
            {
                StartExit();
            }
        }
    }

    private void StartExit()
    {
        float verticalDirection = Random.value < 0.5f ? 1f : -1f;
        Vector3 exitDirection = new Vector3(1f, verticalDirection, 0f).normalized;
        moveDirection = exitDirection;
        currentState = ChaserState.Exiting;
    }

    private void ExitArena()
    {
        transform.position += moveDirection * exitSpeed * Time.fixedDeltaTime;
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPosition.x > 1.1f || screenPosition.y > 1.1f || screenPosition.y < -0.1f)
        {
            RespawnFromRight();
        }
    }

    private void RespawnFromRight()
    {
        float bottomY = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        float topY = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        float randomY = Random.Range(bottomY, topY);
        transform.position = new Vector3(rightEdge + 1f, randomY, 0f);
        setTarget();
    }
}
