using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthComponent>().TakeDamage(damage);
            gameObject.SetActive(false);

            if (gameObject.GetComponentInParent<Minotaur>() != null)
            {
                gameObject.GetComponentInParent<Minotaur>().playerHit = true;
            }
        }
    }
}
