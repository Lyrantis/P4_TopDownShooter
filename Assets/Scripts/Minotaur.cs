using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Minotaur : MonoBehaviour
{

    [SerializeField] GameObject trophy;
    [Header("NavMesh")]

    [SerializeField] NavMeshAgent agent;

    [Header("Hitboxes")]

    [SerializeField] GameObject spinAttackBox;
    [SerializeField] GameObject jabAttackBox;
    [SerializeField] GameObject swingAttackBox;

    [Header("Attacks")]

    [SerializeField] int spinDamage;
    [SerializeField] int jabDamage;
    [SerializeField] int swingDamage;

    public float chargeSpeed = 6.0f;
    private Vector2 chargeDirection = Vector2.zero;

    [SerializeField] float actionCooldown = 5.0f;
    private bool ableToAct = true;
    private bool ableToMove = false;
    private bool charging = false;
    public bool playerHit = false;
    private float maxChargingTime = 3.0f;
    private float chargeTime;
    private bool BossActive = false;

    [Header("Bat Spawns")]

    [SerializeField] GameObject bat;
    [SerializeField] List<GameObject> batSpawnPoints;

    private Animator anim;

    private GameObject player;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        spinAttackBox.SetActive(false);
        jabAttackBox.SetActive(false);
        swingAttackBox.SetActive(false);

        anim = GetComponent<Animator>();
        anim.SetBool("Idle", true);


        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        playerHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (player != null)
        {
            if (target.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null && BossActive)
        {
            if (charging)
            {
                transform.position = new Vector2(transform.position.x + (chargeDirection.x * chargeSpeed * Time.deltaTime), transform.position.y + (chargeDirection.y * chargeSpeed * Time.deltaTime));
                chargeTime += Time.deltaTime;

                if (chargeTime >= maxChargingTime || playerHit)
                {
                    charging = false;
                    StartCoroutine(StopActions(0.0f));
                }
            }
            else if (ableToAct)
            {
                ableToAct = false;
                float distanceToPlayer = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).magnitude;

                ableToMove = false;
                anim.SetBool("Running", false);

                if (distanceToPlayer <= 3.0f)
                {
                    PickCloseAction();
                }
                else
                {
                    PickFarAction();
                }
            }
            else if (ableToMove)
            {
                agent.SetDestination(target.position);
            }
            else
            {

            }
        }
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void PickCloseAction()
    {

        int action = Random.Range(1, 4);
        float actionTime = 0.0f;

        switch (action)
        {
            case 1:

                jabAttackBox.SetActive(true);
                actionTime = 0.35f;
                break;

            case 2:

                spinAttackBox.SetActive(true);
                actionTime = 0.6f;
                break;

            case 3:

                swingAttackBox.SetActive(true);
                actionTime = 0.6f;
                break;

            default:
                break;
        }

        anim.SetInteger("Attacking", action);
        StartCoroutine(StopActions(actionTime));

    }

    private void PickFarAction()
    {

        int action = Random.Range(0, 2);
        float actionTime = 0.0f;

        switch (action)
        {
            case 0:

                anim.SetBool("Charging", true);
                StartCoroutine(Charge());
                break;

            case 1:

                spinAttackBox.SetActive(true);
                actionTime = 1.5f;
                anim.SetInteger("Attacking", 4);
                StartCoroutine(SpawnBats(actionTime));
                break;

            default:
                break;
        }

        
    }

    IEnumerator SpawnBats(float spawnTime)
    {
        for (int i = 0; i < batSpawnPoints.Count; i++)
        {
            yield return new WaitForSeconds(spawnTime / batSpawnPoints.Count);
            GameObject spawnedBat = Instantiate(bat, batSpawnPoints[i].transform.position, transform.rotation);
        }

        StartCoroutine(StopActions(0.0f));
    }

    IEnumerator Charge()
    {
        yield return new WaitForSeconds(1.5f);

        jabAttackBox.SetActive(true);
        chargeDirection = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
        charging = true;
        anim.SetBool("Charging", false);
        anim.SetInteger("Attacking", 1);
        chargeTime = 0.0f;
    }

    IEnumerator StopActions(float attackTime)
    {
        yield return new WaitForSeconds(attackTime);

        spinAttackBox.SetActive(false);
        jabAttackBox.SetActive(false);
        swingAttackBox.SetActive(false);

        anim.SetInteger("Attacking", 0);
        anim.SetBool("Charging", false);

        ableToMove = true;
        playerHit = false;

        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(actionCooldown);

        ableToAct = true;
    }

    public void Die()
    {
        anim.SetBool("Dead", true);

        Vector3 trophyPos = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
        Instantiate(trophy, trophyPos, transform.rotation);
        Destroy(this);
    }

    public void Activate()
    {
        BossActive = true;
    }
}
