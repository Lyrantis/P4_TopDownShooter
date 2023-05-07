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

                }
                //Disable player movement
                //Pick 2 random upgrades 
                //Show Upgrade Options
            }
        }
    }
}
