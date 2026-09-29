using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.15f;
    public float magnitude = 0.15f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void StopShake()
    {
        StopAllCoroutines();
        transform.localPosition = originalPosition;
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = originalPosition + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
