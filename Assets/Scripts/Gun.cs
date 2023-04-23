using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float projectileSpeed;
    private float shotDelay;
    private int projectileCount;
    private int damage;
    private float range;
    private Vector2 direction;
    private int ammoCount;
    private int maxAmmo;
    private float spreadAngle;

    public enum WeaponType
    {
        Pistol,
        Shotgun,
        Rifle,
        SMG
    }
    public WeaponType type;

    public int level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
