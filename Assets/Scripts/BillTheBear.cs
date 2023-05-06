using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BillTheBear : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] GameObject attackBox;

    public float aggroDistance = 10.0f;
    private bool aggro = false;
    private bool moving = false;
    private bool canAttack = true;
    private float attackBoxOffset;
    private float attackRange = 3.0f;
    public float attackCooldown = 1.0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        attackBoxOffset = attackBox.transform.localPosition.x;
        attackBox.SetActive(false);
        anim = GetComponent<Animator>();
        anim.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).magnitude;

        if (distanceToPlayer < aggroDistance || GetComponent<HealthComponent>().GetHealth() < GetComponent<HealthComponent>().maxHealth)
        {
            aggro = true;
            moving = true;
            anim.SetBool("Moving", true);
        }

        if (aggro && moving)
        {
            if (transform.position.x < target.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                attackBox.transform.localPosition = new Vector2(-attackBoxOffset, 0);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                attackBox.transform.localPosition = new Vector2(attackBoxOffset, 0); 
            }

            if (distanceToPlayer <= attackRange && canAttack)
            {
                canAttack = false;
                attackBox.SetActive(true);
                moving = false;
                anim.SetBool("Attacking", true);
                StartCoroutine(StopAttack(1.0f));
                
            }

            agent.SetDestination(target.position);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator StopAttack(float timeToAttack)
    {
        yield return new WaitForSeconds(timeToAttack);

        attackBox.SetActive(false);
        anim.SetBool("Attacking", false);
        StartCoroutine(AttackCooldown());
        moving = true;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
