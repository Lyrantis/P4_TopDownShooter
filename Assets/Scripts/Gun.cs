using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int ammoCount;
    public int currentAmmoLoaded;
    public int maxAmmoLoaded;
    public int maxAmmo;
    private bool noReload = false;
    public GameObject projectile;
    public GameObject alternateProjectile;
    public GameObject firePoint;
    public float maxSpreadAngle;
    public float range = 10.0f;
    public bool isFiring = false;
    private bool tripleShot = false;
    private bool doubleShot = false;
    private bool isReloading = false;
    public Projectile.projectileEffects bulletEffect = Projectile.projectileEffects.None;
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
        if (!noReload)
        {
            if (ammoCount >= (maxAmmoLoaded - currentAmmoLoaded))
            {
                ammoCount -= (maxAmmoLoaded - currentAmmoLoaded);
                currentAmmoLoaded = maxAmmoLoaded;
            }
            else
            {
                currentAmmoLoaded += ammoCount;
                ammoCount = 0;
            }
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

                    Vector2 rotatedDirection1 = new Vector2(distance.x * Mathf.Cos(angleToAdjust + (3.0f * Mathf.Deg2Rad)) - distance.y * Mathf.Sin(angleToAdjust + (3.0f * Mathf.Deg2Rad)), distance.x * Mathf.Sin(angleToAdjust + (3.0f * Mathf.Deg2Rad)) + distance.y * Mathf.Cos(angleToAdjust + (3.0f * Mathf.Deg2Rad)));
                    rotatedDirection1.Normalize();
                    Vector2 rotatedDirection2 = new Vector2(distance.x * Mathf.Cos(angleToAdjust - (3.0f * Mathf.Deg2Rad)) - distance.y * Mathf.Sin(angleToAdjust - (3.0f * Mathf.Deg2Rad)), distance.x * Mathf.Sin(angleToAdjust - (3.0f * Mathf.Deg2Rad)) + distance.y * Mathf.Cos(angleToAdjust - (3.0f * Mathf.Deg2Rad)));
                    rotatedDirection2.Normalize();

                    GameObject bullet;

                    if (tripleShot)
                    {
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
                        bullet.GetComponent<Projectile>().effect = bulletEffect;
                        bullet.transform.SetParent(null);

                        if (altFire)
                        {
                            bullet = Instantiate(alternateProjectile, firePoint.transform);
                        }
                        else
                        {
                            bullet = Instantiate(projectile, firePoint.transform);
                        }


                        bullet.GetComponent<Projectile>().SetDirection(rotatedDirection1);
                        bullet.GetComponent<Projectile>().SetPlayerProjectile(true);
                        bullet.GetComponent<Projectile>().MultiplyDamage(damageMultiplier);
                        bullet.GetComponent<Projectile>().SetRange(range);
                        bullet.GetComponent<Projectile>().effect = bulletEffect;
                        bullet.transform.SetParent(null);

                        if (altFire)
                        {
                            bullet = Instantiate(alternateProjectile, firePoint.transform);
                        }
                        else
                        {
                            bullet = Instantiate(projectile, firePoint.transform);
                        }


                        bullet.GetComponent<Projectile>().SetDirection(rotatedDirection2);
                        bullet.GetComponent<Projectile>().SetPlayerProjectile(true);
                        bullet.GetComponent<Projectile>().MultiplyDamage(damageMultiplier);
                        bullet.GetComponent<Projectile>().SetRange(range);
                        bullet.GetComponent<Projectile>().effect = bulletEffect;
                        bullet.transform.SetParent(null);
                    }
                    else
                    {
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
                        bullet.GetComponent<Projectile>().effect = bulletEffect;
                        bullet.transform.SetParent(null);
                    }
                    
                }
                
                currentAmmoLoaded -= 1;

                if (doubleShot)
                {
                    StartCoroutine(FollowUpShot());
                }
               
            }
            else
            {
                isReloading = true;
            }

            currentShotDelay = shotDelay;

        }
    }

    IEnumerator FollowUpShot()
    {
        yield return new WaitForSeconds(0.25f);

        if (currentAmmoLoaded > 0)
        {
            Vector3 MousePos = Mouse.current.position.ReadValue();
            MousePos.z = Camera.main.nearClipPlane;

            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            Vector2 distance = new Vector2(MousePos.x - gameObject.transform.position.x, MousePos.y - gameObject.transform.position.y);
            float angleRad = maxSpreadAngle * Mathf.Deg2Rad;
            float angleToAdjust = Random.Range(-angleRad, angleRad);
            Vector2 rotatedDirection = new Vector2(distance.x * Mathf.Cos(angleToAdjust) - distance.y * Mathf.Sin(angleToAdjust), distance.x * Mathf.Sin(angleToAdjust) + distance.y * Mathf.Cos(angleToAdjust));
            rotatedDirection.Normalize();

            Vector2 rotatedDirection1 = new Vector2(distance.x * Mathf.Cos(angleToAdjust + 3.0f) - distance.y * Mathf.Sin(angleToAdjust + 3.0f), distance.x * Mathf.Sin(angleToAdjust + 3.0f) + distance.y * Mathf.Cos(angleToAdjust + 3.0f));
            Vector2 rotatedDirection2 = new Vector2(distance.x * Mathf.Cos(angleToAdjust - 3.0f) - distance.y * Mathf.Sin(angleToAdjust - 3.0f), distance.x * Mathf.Sin(angleToAdjust - 3.0f) + distance.y * Mathf.Cos(angleToAdjust - 3.0f));

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
            bullet.GetComponent<Projectile>().effect = bulletEffect;
            bullet.transform.SetParent(null);

            currentAmmoLoaded--;
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
                reloadTime = 1.2f;
                range = 10.0f;
                break;

            case WeaponType.Shotgun:

                maxAmmo = 30;
                maxAmmoLoaded = 5;
                projectileCount = 10;
                shotDelay = 1.5f;
                maxSpreadAngle = 5.0f;
                reloadTime = 2.5f;
                range = 5.0f;
                break;

            case WeaponType.AssaultRifle:

                maxAmmo = 240;
                maxAmmoLoaded = 30;
                projectileCount = 1;
                shotDelay = 0.1f;
                maxSpreadAngle = 4.0f;
                reloadTime = 2.0f;
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
        if (noReload)
        {
            currentAmmoLoaded = maxAmmoLoaded;
        }
        else
        {
            ammoCount = maxAmmo;
            currentAmmoLoaded = maxAmmoLoaded;
        }
        
    }

    public void PickupUpgrade(WeaponUpgrade.Upgrades upgradeType)
    {
        if (upgradeType == WeaponUpgrade.Upgrades.P_FireDamage || upgradeType == WeaponUpgrade.Upgrades.AR_FireDamage)
        {
            bulletEffect = Projectile.projectileEffects.Fire;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.P_StatBoost)
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
        else if (upgradeType == WeaponUpgrade.Upgrades.P_SlowingBullets || upgradeType == WeaponUpgrade.Upgrades.SH_SlowingBullets)
        {
            bulletEffect = Projectile.projectileEffects.Slow;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.P_NoReload || upgradeType == WeaponUpgrade.Upgrades.AR_NoReload || upgradeType == WeaponUpgrade.Upgrades.SH_NoReload)
        {
            maxAmmoLoaded = maxAmmo;
            currentAmmoLoaded = ammoCount;
            maxAmmo = 0;
            noReload = true;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.P_BigBullets)
        {
            //damageMultiplier *= 5.0f;
            shotDelay = 1.0f;
            range += 20.0f;
            altFire = true;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.AR_TripleShot)
        {
            tripleShot = true;
            damageMultiplier *= 0.5f;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.AR_StatBoost)
        {
            maxAmmoLoaded += 5;
            maxAmmo = maxAmmoLoaded * 12;

            if (shotDelay > 0.02f)
            {
                shotDelay -= 0.02f;
            }

            if (maxSpreadAngle > 0)
            {
                maxSpreadAngle -= 0.5f;
            }

            if (reloadTime > 1.0f)
            {
                reloadTime -= 0.25f;
            }

            damageMultiplier *= 1.2f;
            range += 5.0f;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.AR_HyperFire)
        {
            shotDelay = 0.02f;
            maxSpreadAngle = 8.0f;
            damageMultiplier = 0.3f;

            maxAmmoLoaded = 80;

            maxAmmo = maxAmmoLoaded * 12;
            ammoCount = maxAmmo;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.SH_DoubleAction)
        {
            doubleShot = true;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.SH_Slugs)
        {
            altFire = true;
            projectileCount = 1;
            maxSpreadAngle = 1.0f;
            range += 10.0f;
        }
        else if (upgradeType == WeaponUpgrade.Upgrades.SH_StatBoost)
        {
            projectileCount += 2;
            damageMultiplier = 1.1f;

            shotDelay -= 0.1f;

            maxAmmoLoaded += 1;
            maxAmmo = maxAmmoLoaded * 12;
        }
    }

    public void StartReload()
    {
        isReloading = true;
        currentReloadTime = reloadTime; 
    }
}