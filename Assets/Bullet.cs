using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);

        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null && hitInfo.name == "Enemy")
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (hitInfo.name == "Level")
        {
            Destroy(gameObject);
        }
    }
}
