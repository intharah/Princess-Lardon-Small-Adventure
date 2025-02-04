using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
        float distance = Vector2.Distance(transform.position,player.transform.position);

        if (distance < 10f)
        {
            timer += Time.deltaTime;

            if (timer > 2f)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

}
