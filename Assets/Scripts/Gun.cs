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
    public float maxSpreadAngle;

    public enum WeaponType
    {
        NotSet,
        Pistol,
        Shotgun,
        Rifle,
        SMG
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
            Debug.Log(currentAmmoLoaded);
            
            if (currentAmmoLoaded > 0)
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    Transform firePoint = GetComponentInChildren<Transform>();
                    Vector3 MousePos = Mouse.current.position.ReadValue();
                    MousePos.z = Camera.main.nearClipPlane;

                    MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                    Vector2 distance = new Vector2(MousePos.x - gameObject.transform.position.x, MousePos.y - gameObject.transform.position.y);
                    float angleRad = maxSpreadAngle * Mathf.Deg2Rad;
                    float angleToAdjust = Random.Range(-angleRad, angleRad);
                    Vector2 rotatedDirection = new Vector2(distance.x * Mathf.Cos(angleToAdjust) - distance.y * Mathf.Sin(angleToAdjust), distance.x * Mathf.Sin(angleToAdjust) + distance.y * Mathf.Cos(angleToAdjust));
                    rotatedDirection.Normalize();

                    GameObject bullet = Instantiate(projectile, firePoint);
                    
                    bullet.GetComponent<Projectile>().SetDirection(rotatedDirection);
                    bullet.GetComponent<Projectile>().SetPlayerProjectile(true);
                }
                
                currentAmmoLoaded -= 1;
            }
            else
            {
                Reload();
            }

            currentShotDelay = shotDelay;
        }
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
                break;

            case WeaponType.Shotgun:

                maxAmmo = 30;
                maxAmmoLoaded = 5;
                projectileCount = 10;
                shotDelay = 1.5f;
                maxSpreadAngle = 5.0f;
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
}
