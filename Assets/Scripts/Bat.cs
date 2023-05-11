using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bat : MonoBehaviour
{

    [SerializeField] int damage = 10;
    private NavMeshAgent agent;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);

        float distanceToPlayer = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).magnitude;

        if (distanceToPlayer < 0.8f)
        {
            player.GetComponent<HealthComponent>().TakeDamage(damage);
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
