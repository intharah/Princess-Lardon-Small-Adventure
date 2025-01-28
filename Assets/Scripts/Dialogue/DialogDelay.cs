using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DialogueDelay());
    }

    private IEnumerator DialogueDelay()
    {
        yield return new WaitForSeconds(3);
    }
}
