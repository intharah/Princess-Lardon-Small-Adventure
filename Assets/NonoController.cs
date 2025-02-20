using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.UI;
using Platformer.Mechanics;

public class NonoController : MonoBehaviour
{
    public Transform player;
    public Animator animator;

    public bool isFlipped = false;

    public void Update()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

         if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;        
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        NonoController nonoController = other.GetComponent<NonoController>();
     
        if (other.name == "CheckpointNono")
        {
                Debug.Log(other.name);
                if (animator != null)
                { 
                    animator.Play("NonoDeath", -1, 0f); // Play once 
                }
                else
                {
                    Debug.LogError("Animator non assigné");
                }
        }
    }
}
