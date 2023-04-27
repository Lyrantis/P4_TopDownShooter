using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SydTheSquid : MonoBehaviour
{
    public enum Direction 
    { 
        Left, Right, Up, Down
    }


    public int health = 50;
    public GameObject projectile;
    public Direction facingDirection;
    private Vector2 projectileDirection;
    private bool flipProjectileSprite = false;
    public float shotDelay;
    private float currentShotDelay;

    // Start is called before the first frame update
    void Start()
    {
        switch (facingDirection) {

            case Direction.Left:

                projectileDirection = Vector2.left;
                break;

            case Direction.Right:

                projectileDirection = Vector2.right;
                flipProjectileSprite = true;   
                break;

            case Direction.Up:

                projectileDirection = Vector2.up;
                break;

            case Direction.Down:
                projectileDirection = Vector2.down;
                break;

            default:

                projectileDirection = new Vector2(0, 0);
                break;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform);
        bullet.GetComponent<Projectile>().SetDirection(projectileDirection);
        bullet.transform.SetParent(null);
        bullet.GetComponent<SpriteRenderer>().flipX = flipProjectileSprite;
        currentShotDelay = shotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        currentShotDelay -= Time.deltaTime;

        if (currentShotDelay <= 0.0f)
        {
            Shoot();
        }
    }
}
