using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject entryText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveEntryText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RemoveEntryText()
    {
        yield return new WaitForSeconds(5.0f);

        entryText.SetActive(false);
    }
}
