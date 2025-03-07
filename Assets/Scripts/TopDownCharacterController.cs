using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownCharacterController : MonoBehaviour
{

    //Reference to attached animator
    private Animator animator;

    public LevelManager levelManager;
 
    //Reference to attached rigidbody 2D
    private Rigidbody2D rb;
    private GameObject gun = null;

    //The direction the player is moving in
    public Vector2 movingDirection;
    public Vector2 facingDirection;

    //The speed at which they're moving
    private float playerSpeed = 1.0f;

    [Header("Movement parameters")]

    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 100.0f;
    public bool hasKey = false;

    [SerializeField] GameObject AmmoCount;


    /// <summary>
    /// When the script first initialises
    /// </summary>
    private void Awake()
    {
        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// When a fixed update cycle is called
    /// </summary>
    private void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied
        //by the speed they're moving
        rb.velocity = movingDirection * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;
        Vector3 MousePos = Mouse.current.position.ReadValue();
        MousePos.z = Camera.main.nearClipPlane;

        MousePos = Camera.main.ScreenToWorldPoint(MousePos);

        Vector2 distance = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);

        if (Vector2.SignedAngle(Vector2.up, distance) > 157.5f) // Facing bottom
        {
            facingDirection = new Vector2(0, -1);

            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(0.0f, -0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) >= 112.5f) // bottom left
        {
            facingDirection = new Vector2(-1, -1);

            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(-0.6f, -0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(-135.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) >= 67.5f) // left
        {
            facingDirection = new Vector2(-1, 0);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(-0.6f, -0.0f);
                gun.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) >= 22.5f) // top left
        {
            facingDirection = new Vector2(-1, 1);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(-0.6f, 0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(135.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) >= 0.0f) // top
        {
            facingDirection = new Vector2(0, 1);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(0.0f, 0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) <= -157.5f) // bottom 
        {
            facingDirection = new Vector2(0, -1);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(0.0f, -0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) <= -112.5f) // bottom right
        {
            facingDirection = new Vector2(1, -1);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(0.6f, -0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(-45.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) <= -67.5f) // right
        {
            facingDirection = new Vector2(1, 0);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(0.6f, 0.0f);
                gun.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) <= -22.5f) // top right
        {
            facingDirection = new Vector2(1, 1);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(0.6f, 0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(45.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        else if (Vector2.SignedAngle(Vector2.up, distance) <= 0.0f) // top
        {
            facingDirection = new Vector2(0, 1);
            if (gun != null)
            {
                gun.transform.localPosition = new Vector2(0.0f, 0.75f);
                gun.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                gun.GetComponent<SpriteRenderer>().flipY = true;
            }
        }

        animator.SetFloat("Horizontal", facingDirection.x);
        animator.SetFloat("Vertical", facingDirection.y);

        if (gun != null)
        {
            AmmoCount.GetComponent<TextMeshProUGUI>().text = "Ammo:\n" + gun.GetComponent<Gun>().currentAmmoLoaded + "/" + gun.GetComponent<Gun>().ammoCount;
        }
        
    }

    public void OnPlayerInputShoot(InputAction.CallbackContext context)
    {
        //Not performed? Don't run any other code
        if (!context.performed)
        {
            if (gun != null)
            {
                gun.GetComponent<Gun>().isFiring = false;

            }
            return;
        }

        //Otherwise:
        if (gun != null)
        {
            gun.GetComponent<Gun>().isFiring = true;
        }
       
    }

    /// <summary>
    /// Called when the player wants to move in a certain direction
    /// </summary>
    /// <param name="context"></param>
    public void OnPlayerInputMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            //Was the action just cancelled (released)? If so, set
            //speed to 0
            playerSpeed = 0.0f;

            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
            return;
        }

        //Otherwise, if the context wasn't performed, don't run
        //the code below
        if (!context.performed)
            return;

        //Read the direction that the player wants to move, from the
        //keys that have been pressed
        Vector2 direction = context.ReadValue<Vector2>();

        //Set the player's direction to whatever it is
        movingDirection = direction;

        //Set animator parameters
        animator.SetFloat("Speed", movingDirection.magnitude);

        //And set the speed to 1, so they move!
        playerSpeed = 1f;
    }

    public void SetGun(GameObject newGun)
    {
        if (gun != null)
        {
            Destroy(gun);
        }

        gun = newGun;
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetComponentInChildren<Gun>().StartReload();
        }
    }

    public void Die()
    {
        levelManager.GameOver();
    }

    public void Win()
    {
        levelManager.Win();
    }

}
