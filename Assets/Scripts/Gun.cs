using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    private float shotDelay;
    private float currentShotDelay = 0.0f;
    private int projectileCount;
    private Vector2 direction;
    private int ammoCount;
    private int currentAmmoLoaded;
    private int maxAmmoLoaded;
    private int maxAmmo;
    public GameObject projectile;
    public GameObject firePoint;
    public float maxSpreadAngle;
    private bool autofire = false;
    private bool isFiring = false;

    public enum WeaponType
    {
        NotSet,
        Pistol,
        Shotgun,
        AssaultRifle,
        SMG,
        RocketLauncher
    }
    public WeaponType type;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShotDelay > 0.0f)
        {
            currentShotDelay -= Time.deltaTime;

            if (currentShotDelay <= 0.0f)
            {
                currentShotDelay = 0.0f;
            }
        }
    }

    void Reload()
    {
        if (ammoCount > maxAmmoLoaded)
        {
            currentAmmoLoaded = maxAmmoLoaded;
            ammoCount -= maxAmmoLoaded;
        }
        else
        {
            currentAmmoLoaded = ammoCount;
            ammoCount = 0;
        }
        
    }

    public void Shoot()
    {
        
        if (currentShotDelay == 0.0f)
        {
            
            if (currentAmmoLoaded > 0)
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    Vector3 MousePos = Mouse.current.position.ReadValue();
                    MousePos.z = Camera.main.nearClipPlane;

                    MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                    Vector2 distance = new Vector2(MousePos.x - gameObject.transform.position.x, MousePos.y - gameObject.transform.position.y);
                    float angleRad = maxSpreadAngle * Mathf.Deg2Rad;
                    float angleToAdjust = Random.Range(-angleRad, angleRad);
                    Vector2 rotatedDirection = new Vector2(distance.x * Mathf.Cos(angleToAdjust) - distance.y * Mathf.Sin(angleToAdjust), distance.x * Mathf.Sin(angleToAdjust) + distance.y * Mathf.Cos(angleToAdjust));
                    rotatedDirection.Normalize();

                    GameObject bullet = Instantiate(projectile, firePoint.transform);
                    
                    bullet.GetComponent<Projectile>().SetDirection(rotatedDirection);
                    bullet.GetComponent<Projectile>().SetPlayerProjectile(true);
                    bullet.transform.SetParent(null);
                }
                
                currentAmmoLoaded -= 1;
            }
            else
            {
                Reload();
            }

            currentShotDelay = shotDelay;

            Debug.Log(type);
            if (autofire)
            {
                Debug.Log("WHAT");
                StartCoroutine(AutoFire(shotDelay));
            }
        }
    }

    public void StopShooting()
    {
        StopAllCoroutines();
        currentShotDelay = shotDelay;
        isFiring = false;
    }

    IEnumerator AutoFire(float timeBetweenShots)
    {
        Debug.Log("FIRING");
        isFiring = true;
        yield return new WaitForSeconds(timeBetweenShots);

        if (currentAmmoLoaded > 0)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                Vector3 MousePos = Mouse.current.position.ReadValue();
                MousePos.z = Camera.main.nearClipPlane;

                MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                Vector2 distance = new Vector2(MousePos.x - gameObject.transform.position.x, MousePos.y - gameObject.transform.position.y);
                float angleRad = maxSpreadAngle * Mathf.Deg2Rad;
                float angleToAdjust = Random.Range(-angleRad, angleRad);
                Vector2 rotatedDirection = new Vector2(distance.x * Mathf.Cos(angleToAdjust) - distance.y * Mathf.Sin(angleToAdjust), distance.x * Mathf.Sin(angleToAdjust) + distance.y * Mathf.Cos(angleToAdjust));
                rotatedDirection.Normalize();

                GameObject bullet = Instantiate(projectile, firePoint.transform);

                bullet.GetComponent<Projectile>().SetDirection(rotatedDirection);
                bullet.GetComponent<Projectile>().SetPlayerProjectile(true);
                bullet.transform.SetParent(null);
            }

            currentAmmoLoaded -= 1;
        }
        else
        {
            Reload();
        }

        AutoFire(timeBetweenShots);

    }

    public void SetType(WeaponType gunType)
    {
        switch (gunType)
        {
            case WeaponType.Pistol:

                maxAmmo = 144;
                maxAmmoLoaded = 12;
                projectileCount = 1;
                shotDelay = 0.5f;
                maxSpreadAngle = 3.0f;
                autofire = false;
                break;

            case WeaponType.Shotgun:

                maxAmmo = 30;
                maxAmmoLoaded = 5;
                projectileCount = 10;
                shotDelay = 1.5f;
                maxSpreadAngle = 5.0f;
                autofire = false;
                break;

            case WeaponType.AssaultRifle:

                maxAmmo = 240;
                maxAmmoLoaded = 30;
                projectileCount = 1;
                shotDelay = 0.1f;
                maxSpreadAngle = 6.0f;
                autofire = true;
                Debug.Log("Autofire has been set");
                break;

            default:
                break;
        }

        currentAmmoLoaded = maxAmmoLoaded;
        ammoCount = maxAmmo;
        currentShotDelay = 0;
    }

    public void SetLevel(int newLevel)
    {

    }

    public bool GetIsFiring()
    {
        return isFiring;
    }
}
