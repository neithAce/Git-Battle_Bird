using UnityEngine;

public class EnemyBattleLargeMovement : MonoBehaviour
{
    public float enterSpeed = 5f;
    public float movementSpeed = 1f;
    private Vector3 targetPosition;
    private bool entering = true;
    private float horizontalRange;
    private float verticalRange;
    private float time;

    private void Start()
    {
        Camera cam = Camera.main;
        float centerX = cam.transform.position.x;
        float centerY = cam.transform.position.y;
        targetPosition = new Vector3(centerX, centerY, transform.position.z);
        float cameraHeight = cam.orthographicSize * 2f;
        float cameraWidth = cameraHeight * cam.aspect;
        horizontalRange = cameraWidth / 2f;
        verticalRange = cameraHeight / 2f;
    }

    private void FixedUpdate()
    {
        if (entering)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enterSpeed * Time.fixedDeltaTime);
            if(Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                entering = false;
                time = 0f;
            }

            return;
        }

        time += movementSpeed * Time.fixedDeltaTime;
        float x = Mathf.Sin(time) * horizontalRange;
        float y = Mathf.Sin(time * 2f) * verticalRange;
        transform.position = targetPosition + new Vector3(x, y, 0f);
    }
}
