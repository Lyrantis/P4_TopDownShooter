using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage;
    public float speed;
    public float range;
    public Vector2 direction;
    public bool explosive;
    public float explosionRadius;
    private bool playerProjectile = false;

    private Vector2 startPos;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction * speed;

        if (new Vector2(transform.position.x - startPos.x, transform.position.y - startPos.y).magnitude >= range)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
        
    }

    public void SetPlayerProjectile(bool isPlayerProjectile)
    {
        playerProjectile = isPlayerProjectile;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Player") && !playerProjectile) || (other.gameObject.CompareTag("Enemy") && playerProjectile))
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
            Destroy(gameObject);
            Destroy(this);
        }
        else if (other.gameObject.CompareTag("Level"))
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        Destroy(this);
    }
}
