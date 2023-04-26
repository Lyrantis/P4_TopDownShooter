using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public Gun.WeaponType type;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponentInChildren<Gun>().SetType(type);
            transform.SetParent(other.transform, true);
            other.GetComponent<TopDownCharacterController>().SetGun(gameObject);
            Destroy(this);
        }
    }

}
