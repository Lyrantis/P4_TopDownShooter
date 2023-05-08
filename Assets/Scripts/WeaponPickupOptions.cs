using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickupOptions : MonoBehaviour
{
    [SerializeField] GameObject player;

    private WeaponUpgrade.Upgrades option1;
    private WeaponUpgrade.Upgrades option2;

    [SerializeField] Image Option1Image;
    [SerializeField] Image Option2Image;


    [SerializeField] List<Sprite> imageList;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetOptions(WeaponUpgrade.Upgrades type1, WeaponUpgrade.Upgrades type2)
    {
        option1 = type1;
        option2 = type2;

        Debug.Log(option1 + ", " + option2);

        switch (option1)
        {
            case WeaponUpgrade.Upgrades.None:
                break;
            case WeaponUpgrade.Upgrades.P_FireDamage: case WeaponUpgrade.Upgrades.AR_FireDamage:
                Option1Image.sprite = imageList[0];
                break;
            case WeaponUpgrade.Upgrades.P_StatBoost: case WeaponUpgrade.Upgrades.AR_StatBoost: case WeaponUpgrade.Upgrades.SH_StatBoost:
                Option1Image.sprite = imageList[1];
                break;
            case WeaponUpgrade.Upgrades.P_SlowingBullets: case WeaponUpgrade.Upgrades.SH_SlowingBullets:
                Option1Image.sprite = imageList[2];
                break;
            case WeaponUpgrade.Upgrades.P_NoReload: case WeaponUpgrade.Upgrades.SH_NoReload: case WeaponUpgrade.Upgrades.AR_NoReload:
                Option1Image.sprite = imageList[3];
                break;
            case WeaponUpgrade.Upgrades.P_BigBullets:
                Option1Image.sprite = imageList[4];
                break;
            case WeaponUpgrade.Upgrades.AR_TripleShot:
                Option1Image.sprite = imageList[5];
                break;
            case WeaponUpgrade.Upgrades.AR_HyperFire:
                Option1Image.sprite = imageList[6];
                break;
            case WeaponUpgrade.Upgrades.SH_DoubleAction:
                Option1Image.sprite = imageList[7];
                break;
            case WeaponUpgrade.Upgrades.SH_Slugs:
                Option1Image.sprite = imageList[8];
                break;
        }

        switch (option2)
        {
            case WeaponUpgrade.Upgrades.None:
                break;
            case WeaponUpgrade.Upgrades.P_FireDamage: case WeaponUpgrade.Upgrades.AR_FireDamage:
                Option2Image.sprite = imageList[0];
                break;
            case WeaponUpgrade.Upgrades.P_StatBoost: case WeaponUpgrade.Upgrades.AR_StatBoost: case WeaponUpgrade.Upgrades.SH_StatBoost:
                Option2Image.sprite = imageList[1];
                break;
            case WeaponUpgrade.Upgrades.P_SlowingBullets: case WeaponUpgrade.Upgrades.SH_SlowingBullets:
                Option2Image.sprite = imageList[2];
                break;
            case WeaponUpgrade.Upgrades.P_NoReload: case WeaponUpgrade.Upgrades.SH_NoReload: case WeaponUpgrade.Upgrades.AR_NoReload:
                Option2Image.sprite = imageList[3];
                break;
            case WeaponUpgrade.Upgrades.P_BigBullets:
                Option2Image.sprite = imageList[4];
                break;
            case WeaponUpgrade.Upgrades.AR_TripleShot:
                Option2Image.sprite = imageList[5];
                break;
            case WeaponUpgrade.Upgrades.AR_HyperFire:
                Option2Image.sprite = imageList[6];
                break;
            case WeaponUpgrade.Upgrades.SH_DoubleAction:
                Option2Image.sprite = imageList[7];
                break;
            case WeaponUpgrade.Upgrades.SH_Slugs:
                Option2Image.sprite = imageList[8];
                break;
        }
    }


    public void PickOption1()
    {
        player.GetComponentInChildren<Gun>().PickupUpgrade(option1);
        gameObject.SetActive(false);
        player.GetComponent<TopDownCharacterController>().enabled = true;
    }

    public void PickOption2()
    {
        player.GetComponentInChildren<Gun>().PickupUpgrade(option2);
        gameObject.SetActive(false);
        player.GetComponent<TopDownCharacterController>().enabled = true;
    }
}
