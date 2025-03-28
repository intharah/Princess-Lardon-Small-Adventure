using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.UI;
using Platformer.Mechanics;

public class Boss : MonoBehaviour
{
    AudioSource _audio;
    public AudioClip Ouch;
    public Transform player;

    public bool isFlipped = false;
    public float health;
    public float maxHealth = 1f;
    private float convertHealth;
    [SerializeField] FloatingHealthBar healthBar;
    public GameObject explosion;
    private GameObject boss;
    public int playerDamage = 10;

    private void Start()
    {
        convertHealth = maxHealth;
        healthBar.UpdateHealthBar(convertHealth, maxHealth);

        boss = GameObject.FindWithTag("Boss");
    }

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Start is called before the first frame update
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

         if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            healthBar.transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            healthBar.transform.Rotate(0f, 180f, 0f);
            isFlipped = true;        
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        convertHealth = health / 100;

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(convertHealth, maxHealth);
        }

        Debug.Log(health);
        Debug.Log(convertHealth);

        if (health <= 0)
        {
            // Remove Boss Weapon to stop firing 
            // GetComponent<BossWeapon>().enabled = false;

            // Add Explosion Animation
            GameObject exploded = Instantiate(explosion, transform.position, Quaternion.identity);


            // Play death clip 
            //_audio.clip = Ouch;
            //_audio.Play();
            
            Destroy(boss);
            Destroy(exploded, 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);

        PlayerController playerController = hitInfo.GetComponent<PlayerController>();

        // Increase Damage Player Health
        if (playerController != null && hitInfo.name == "Rach")
        {
            playerController.TakeDamage(playerDamage);
        }
    }
}
