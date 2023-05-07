using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupOptions : MonoBehaviour
{

    public WeaponUpgrade.Upgrades option1;
    public WeaponUpgrade.Upgrades option2;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickOption1()
    {
        player.GetComponent<Gun>().PickupUpgrade(option1);
    }

    public void PickOption2()
    {
        player.GetComponent<Gun>().PickupUpgrade(option2);
    }
}
