using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

public class Boss : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;

    // Start is called before the first frame update
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
        if(other.gameObject.CompareTag("Player"))
        {
            Schedule<PlayerDeath>();
        }
    }
}
