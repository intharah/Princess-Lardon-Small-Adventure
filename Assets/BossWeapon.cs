using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2f)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

}
