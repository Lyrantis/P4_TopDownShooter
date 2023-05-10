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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthComponent>().TakeDamage(damage);
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
