using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<TopDownCharacterController>().hasKey)
            {
                anim.SetBool("Open", true);
                StartCoroutine(RemoveHitbox(0.75f));
            }
        }
    }

    IEnumerator RemoveHitbox(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);

        Destroy(GetComponentInParent<BoxCollider2D>());
        //Destroy(gameObject);
        //Destroy(this);
    }

}
