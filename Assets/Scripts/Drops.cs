using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    [SerializeField] List<GameObject> CommonDrops = new List<GameObject>();
    [SerializeField] List<GameObject> RareDrops = new List<GameObject>();

    // Start is called before the first frame update
    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            int random = Random.Range(0, 100);
            int randObject;

            if (random <= 5)
            {
                randObject = Random.Range(0, RareDrops.Count);
                Instantiate(RareDrops[randObject], transform);
            }
            else if (random <= 30)
            {
                randObject = Random.Range(0, CommonDrops.Count);
                Instantiate(CommonDrops[randObject], transform);
            }
        }

    }
}
