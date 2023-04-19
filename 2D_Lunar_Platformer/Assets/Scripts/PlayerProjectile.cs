using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }

        
           
            Destroy(gameObject);
        

    }
}
