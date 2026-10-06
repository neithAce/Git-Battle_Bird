using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public AudioClip shootSFX;
    private float nextFireTime = 0f;
    private bool tripleShotActive = false;
    private float tripleShotTimer = 0f;

    private void Update()
    {
        if (tripleShotActive)
        {
            tripleShotTimer -= Time.deltaTime;
            if (tripleShotTimer <= 0f)
            {
                tripleShotActive = false;
            }
        }

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(shootSFX);
        }

        if (tripleShotActive) {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, 25f));
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, -25f));
        } 
        else 
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void ActivateTripleShot()
    {
        tripleShotActive = true;
        tripleShotTimer = 20f;
    }

    public bool IsTripleShotActive()
    {
        return tripleShotActive;
    }

    public float GetTripleShotRemainingTime()
    {
        return tripleShotTimer;
    }
}
