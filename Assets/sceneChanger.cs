using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.CompareTag("Player");
        if (p != null){
            SceneManager.LoadScene(sceneName);
        }
    }
}

