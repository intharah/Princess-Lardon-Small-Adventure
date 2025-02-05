using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AudioSource _audio;
    public AudioClip Ouch;
    public int health = 10;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        Debug.Log(health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Set Rigidbody2d with Dynamic and unset the Collider2d component to emulate enemy falling down
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;

        // Play death clip 
        _audio.clip = Ouch;
        _audio.Play();
        
        //Destroy(gameObject);
    }
}
