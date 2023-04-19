using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float shootSpeed=0.5f;
    private float BulletSpeed=0.03f;
    private bool canShoot = true;
    public PlayerController player;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && canShoot)
        {
            StartCoroutine(ShootGun());
        }

        if (player.DoubleOn == true)
        {
            shootSpeed = 0.2f;
        }
        else
        {
            shootSpeed = 0.5f;
        }
    }


    public IEnumerator ShootGun()
    {
        
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * BulletSpeed);
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;

    }



}
