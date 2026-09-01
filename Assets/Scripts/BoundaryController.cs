using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform boundaryTop;
    public Transform boundaryBottom;
    public Transform boundaryLeft;
    public Transform boundaryRight;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        UpdateBoundaries();
    }

    private void update()
    {
        UpdateBoundaries();
    }

    private void UpdateBoundaries()
    {
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;
        Vector3 cameraPosition = mainCamera.transform.position;
        boundaryTop.position = new Vector3(cameraPosition.x, cameraPosition.y + halfHeight, boundaryTop.position.z);
        boundaryBottom.position = new Vector3(cameraPosition.x, cameraPosition.y - halfHeight, boundaryBottom.position.z);
        boundaryLeft.position = new Vector3(cameraPosition.x - halfWidth, cameraPosition.y, boundaryLeft.position.z);
        boundaryRight.position = new Vector3(cameraPosition.x + halfWidth, cameraPosition.y, boundaryRight.position.z);
    }
}