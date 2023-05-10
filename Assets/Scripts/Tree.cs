using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Tree : MonoBehaviour
{
    private GameObject player;

    [SerializeField] GameObject bat;
    [SerializeField] GameObject batSpawnPoint;
    [SerializeField] float timeBetweenWaves = 3.0f;
    [SerializeField] int minBatsPerWave = 1;
    [SerializeField] int maxBatsPerWave = 3;

    [SerializeField] float aggroDistance = 5.0f;
    private bool active = false;
    private bool canSpawnBats = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).magnitude;

        if (distanceToPlayer <= aggroDistance)
        {
            active = true;
        }

        if (active)
        {
            if (canSpawnBats)
            {
                SpawnBats();
            }
            else
            {
                //move
            }
        }

        if (player.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void SpawnBats()
    {
        canSpawnBats = false;

        int r = Random.Range(minBatsPerWave, maxBatsPerWave + 1);

        for (int i = 0; i < r; i++)
        {
            Instantiate(bat, batSpawnPoint.transform);
            bat.transform.parent = null;
        }
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        canSpawnBats = true;
    }
}
