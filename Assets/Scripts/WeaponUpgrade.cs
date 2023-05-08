using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public enum Upgrades
    {
        None,
        P_FireDamage,
        P_StatBoost,
        P_SlowingBullets,
        P_NoReload,
        P_BigBullets,
        AR_FireDamage,
        AR_TripleShot,
        AR_StatBoost,
        AR_NoReload,
        AR_HyperFire,
        SH_DoubleAction,
        SH_StatBoost,
        SH_SlowingBullets,
        SH_NoReload,
        SH_Slugs
    }

    Upgrades option1;
    Upgrades option2;

    GameObject optionHUD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Gun>() != null)
            {
                Gun gun = collision.GetComponent<Gun>();

                int random1 = Random.Range(1, 6);
                int random2;

                do
                {
                    random2 = Random.Range(1, 6);

                } while (random2 == random1);

                if (gun.type == Gun.WeaponType.Pistol)
                {
                    switch (random1)
                    {
                        case 1:

                            option1 = Upgrades.P_FireDamage;
                            break;

                        case 2:

                            option1 = Upgrades.P_StatBoost;
                            break;

                        case 3:

                            option1 = Upgrades.P_NoReload;
                            break;

                        case 4:

                            option1 = Upgrades.P_SlowingBullets;
                            break;

                        case 5:

                            option1= Upgrades.P_BigBullets;
                            break;


                    } 
                    
                    switch (random2)
                    {
                        case 1:

                            option2 = Upgrades.P_FireDamage;
                            break;

                        case 2:

                            option2 = Upgrades.P_StatBoost;
                            break;

                        case 3:

                            option2 = Upgrades.P_NoReload;
                            break;

                        case 4:

                            option2 = Upgrades.P_SlowingBullets;
                            break;

                        case 5:

                            option2= Upgrades.P_BigBullets;
                            break;


                    }

                }
                
                if (gun.type == Gun.WeaponType.AssaultRifle)
                {
                    switch (random1)
                    {
                        case 1:

                            option1 = Upgrades.AR_FireDamage;
                            break;

                        case 2:

                            option1 = Upgrades.AR_StatBoost;
                            break;

                        case 3:

                            option1 = Upgrades.AR_NoReload;
                            break;

                        case 4:

                            option1 = Upgrades.AR_TripleShot;
                            break;

                        case 5:

                            option1= Upgrades.AR_HyperFire;
                            break;


                    }

                    switch (random2)
                    {
                        case 1:

                            option2 = Upgrades.AR_FireDamage;
                            break;

                        case 2:

                            option2 = Upgrades.AR_StatBoost;
                            break;

                        case 3:

                            option2 = Upgrades.AR_NoReload;
                            break;

                        case 4:

                            option2 = Upgrades.AR_TripleShot;
                            break;

                        case 5:

                            option2= Upgrades.AR_HyperFire;
                            break;


                    }

                }
                
                if (gun.type == Gun.WeaponType.Shotgun)
                {
                    switch (random1)
                    {
                        case 1:

                            option1 = Upgrades.SH_DoubleAction;
                            break;

                        case 2:

                            option1 = Upgrades.SH_StatBoost;
                            break;

                        case 3:

                            option1 = Upgrades.SH_NoReload;
                            break;

                        case 4:

                            option1 = Upgrades.SH_SlowingBullets;
                            break;

                        case 5:

                            option1= Upgrades.SH_Slugs;
                            break;


                    }
                    
                    switch (random2)
                    {
                        case 1:

                            option2 = Upgrades.SH_DoubleAction;
                            break;

                        case 2:

                            option2 = Upgrades.SH_StatBoost;
                            break;

                        case 3:

                            option2 = Upgrades.SH_NoReload;
                            break;

                        case 4:

                            option2 = Upgrades.SH_SlowingBullets;
                            break;

                        case 5:

                            option2= Upgrades.SH_Slugs;
                            break;


                    }

                }

                optionHUD.SetActive(true);
                optionHUD.GetComponent<WeaponPickupOptions>().SetOptions(option1, option2);
            }
        }
    }
}
