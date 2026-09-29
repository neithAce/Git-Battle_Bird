using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public string loopTag = "Mountain";
    public float loopSpacing = 12f;
    public float leftLimit = -12f;

    private void FixedUpdate()
    {
        transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
        float leftCameraEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        if(GetComponent<SpriteRenderer>().bounds.max.x < leftCameraEdge)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(loopTag);
            float rightmostX = transform.position.x;
            foreach(GameObject obj in objects)
            {
                if(obj.transform.position.x > rightmostX)
                {
                    rightmostX = obj.transform.position.x;
                }
            }

            transform.position = new Vector3(rightmostX + loopSpacing, transform.position.y, transform.position.z);
        }
    }
}
