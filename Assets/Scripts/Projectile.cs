using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage;
    public float speed;
    public Vector2 direction;
    public bool explosive;
    public float explosionRadius;
    public bool playerProjectile;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(direction * speed);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    private void OnTriggerEnter2D(Collider other)
    {
        if ((other.CompareTag("Player") && !playerProjectile) || (other.CompareTag("Enemy") && playerProjectile))
        {
            other.GetComponent<HealthComponent>().TakeDamage(damage);
        }
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}
