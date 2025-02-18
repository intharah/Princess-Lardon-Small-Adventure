using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int damage = 10;
    public int bossDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Boss boss = hitInfo.GetComponent<Boss>();

        // Kill Standard Enemies
        if (enemy != null && hitInfo.name == "Enemy")
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Increase Damage Boss Health
        if (boss != null && hitInfo.name == "Scutigerard")
        {
            boss.TakeDamage(bossDamage);
            Destroy(gameObject);
        }

        // Destroy Bullet When Hitting Level
        if (hitInfo.name == "Level")
        {
            Destroy(gameObject);
        }
    }
}
