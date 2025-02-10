using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        // Get the animator component
        animator = GetComponent<Animator>();

        // Run the help animation
        animator.Play("IntHelp");
    }
}
