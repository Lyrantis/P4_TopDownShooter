using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public enum BulletEffects
    {
        None,
        Fire,
        Slowing
    }

    private float damageMultiplier = 1.0f;
    private float shotDelay;
    private float currentShotDelay = 0.0f;
    public float reloadTime;
    private float currentReloadTime;
    private int projectileCount;
    private Vector2 direction;
    private int ammoCount;
    private int currentAmmoLoaded;
    private int maxAmmoLoaded;
    private int maxAmmo;
    public GameObject projectile;
    public GameObject alternateProjectile;
    public GameObject firePoint;
    public float maxSpreadAngle;
    public float range = 10.0f;
    private bool autofire = false;
    public bool isFiring = false;
    private bool isReloading = false;
    public BulletEffects bulletEffect;
    private bool altFire = false;

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
        if (isReloading)
        {
            currentReloadTime -= Time.deltaTime;

            if (currentReloadTime <= 0.0f)
            {
                currentReloadTime = reloadTime;
                isReloading = false;
                Reload();
            }
        }
        if (currentShotDelay > 0)
        {
            currentShotDelay -= Time.deltaTime;

            if (currentShotDelay <= 0.0f)
            {
                currentShotDelay = 0.0f;
            }
        }

        if (isFiring && !isReloading && currentShotDelay == 0.0f)
        {
            Shoot();
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

                    GameObject bullet;

                    if (altFire)
                    {
                        bullet = Instantiate(alternateProjectile, firePoint.transform);
                    }
                    else
                    {
                        bullet = Instantiate(projectile, firePoint.transform);
                    }
                    
                    
                    bullet.GetComponent<Projectile>().SetDirection(rotatedDirection);
                    bullet.GetComponent<Projectile>().SetPlayerProjectile(true);
                    bullet.GetComponent<Projectile>().MultiplyDamage(damageMultiplier);
                    bullet.GetComponent<Projectile>().SetRange(range);
                    bullet.transform.SetParent(null);
                }
                
                currentAmmoLoaded -= 1;
            }
            else
            {
                isReloading = true;
            }

            currentShotDelay = shotDelay;

        }
    }

    public void StopShooting()
    {
        StopAllCoroutines();
        currentShotDelay = shotDelay;
        isFiring = false;
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
                reloadTime = 2.0f;
                range = 10.0f;
                break;

            case WeaponType.Shotgun:

                maxAmmo = 30;
                maxAmmoLoaded = 5;
                projectileCount = 10;
                shotDelay = 1.5f;
                maxSpreadAngle = 5.0f;
                autofire = false;
                reloadTime = 3.0f;
                range = 5.0f;
                break;

            case WeaponType.AssaultRifle:

                maxAmmo = 240;
                maxAmmoLoaded = 30;
                projectileCount = 1;
                shotDelay = 0.1f;
                maxSpreadAngle = 4.0f;
                autofire = true;
                reloadTime = 3.0f;
                range = 15.0f;
                break;

            default:
                break;
        }

        currentAmmoLoaded = maxAmmoLoaded;
        ammoCount = maxAmmo;
        currentShotDelay = 0;
        currentReloadTime = reloadTime;
    }

    public void SetLevel(int newLevel)
    {

    }

    public bool GetIsFiring()
    {
        return isFiring;
    }

    public void RefillAmmo()
    {
        ammoCount = maxAmmo;
        currentAmmoLoaded = maxAmmoLoaded;
    }

    public void PickupPistolUpgrade(WeaponUpgrade.PistolUpgrades upgradeType)
    {
        if (upgradeType == WeaponUpgrade.PistolUpgrades.FireDamage)
        {
            bulletEffect = BulletEffects.Fire;
        }
        else if (upgradeType == WeaponUpgrade.PistolUpgrades.Akimbo)
        {
            //Akimbo
        }
        else if (upgradeType == WeaponUpgrade.PistolUpgrades.StatBoost)
        {

            maxAmmoLoaded += 3;
            maxAmmo = maxAmmoLoaded * 12;

            if (shotDelay > 0.2f)
            {
                shotDelay -= 0.1f;
            }
            
            if (maxSpreadAngle > 0)
            {
                maxSpreadAngle -= 0.5f;
            }
            
            if (reloadTime > 1.0f)
            {
                reloadTime -= 0.2f;
            }
            
            damageMultiplier *= 1.2f;
            range += 5.0f;
        }
        else if (upgradeType == WeaponUpgrade.PistolUpgrades.SlowingBullets)
        {
            bulletEffect = BulletEffects.Slowing;
        }
        else if (upgradeType == WeaponUpgrade.PistolUpgrades.NoReload)
        {
            maxAmmoLoaded = maxAmmo;
            currentAmmoLoaded = ammoCount;
        }
        else if (upgradeType == WeaponUpgrade.PistolUpgrades.BigBullets)
        {
            //damageMultiplier *= 5.0f;
            shotDelay = 1.0f;
            range += 20.0f;
            altFire = true;
        }
    }

    public void PickupAssaultRifleUpgrade(WeaponUpgrade.AssaultRifleUpgrades upgradeTypee)
    {

    }

    public void PickupShotgunUpgrade(WeaponUpgrade.ShotgunUpgrades upgradeType)
    {

    }
}