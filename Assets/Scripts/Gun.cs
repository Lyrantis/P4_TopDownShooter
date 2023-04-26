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
    public GameObject projectileToFire;
    public float maxSpreadAngle;

    public enum WeaponType
    {
        NotSet,
        Pistol,
        Shotgun,
        Rifle,
        SMG
    }
    public WeaponType StartingType;
    private WeaponType type = WeaponType.NotSet;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetType(WeaponType.Pistol);
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
                    Vector3 MousePos = Mouse.current.position.ReadValue();
                    MousePos.z = Camera.main.nearClipPlane;

                    MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                    Vector2 distance = new Vector2(MousePos.x - gameObject.transform.position.x, MousePos.y - gameObject.transform.position.y);
                    float angleRad = maxSpreadAngle * Mathf.Deg2Rad;
                    float angleToAdjust = Random.Range(-angleRad, angleRad);
                    Vector2 rotatedDirection = new Vector2(distance.x * Mathf.Cos(angleToAdjust) - distance.y * Mathf.Sin(angleToAdjust), distance.x * Mathf.Sin(angleToAdjust) + distance.y * Mathf.Cos(angleToAdjust));
                    rotatedDirection.Normalize();
                    //float linearDistance = distance.magnitude;
                    //float maxOffset = Mathf.Tan(maxSpreadAngle) * linearDistance;
                    //float offset = Random.Range(-maxOffset, maxOffset);

                    //float xRatio;
                    //float yRatio;

                    //if (distance.x == 0)
                    //{
                    //    xRatio = 0;
                    //    yRatio = 1;
                    //}
                    //else if (distance.y == 0)
                    //{
                    //    xRatio = 1;
                    //    yRatio = 0;
                    //}
                    //else
                    //{
                    //    xRatio = Mathf.Abs(distance.x) / Mathf.Abs(distance.y);
                    //    yRatio = 1 - xRatio;
                    //}
                    //Vector2 ratio = new Vector2(xRatio, yRatio);
                    //ratio.Normalize();

                    //Vector2 target = new Vector2(MousePos.x + (offset * ratio.x), MousePos.y + (offset * ratio.y));
                    //distance = new Vector2(target.x - gameObject.transform.position.x, target.y - gameObject.transform.position.y);

                    GameObject bullet = Instantiate(projectileToFire, transform);
                    
                    bullet.GetComponent<Projectile>().SetDirection(rotatedDirection);
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

    public void SetType(WeaponType type)
    {
        if (type != this.type)
        {
            this.type = type;
            level = 1;

            switch (this.type)
            {
                case WeaponType.Pistol:

                    maxAmmo = 144;
                    maxAmmoLoaded = 12;
                    projectileCount = 10;
                    shotDelay = 0.5f;
                    break;

                case WeaponType.Shotgun:

                    maxAmmo = 30;
                    maxAmmoLoaded = 5;
                    projectileCount = 5;
                    shotDelay = 1.5f;
                    break;

                default:
                    break;
            }

        }

        currentAmmoLoaded = maxAmmoLoaded;
        ammoCount = maxAmmo;
        currentShotDelay = 0;
    }

    public void SetLevel(int newLevel)
    {

    }
}
