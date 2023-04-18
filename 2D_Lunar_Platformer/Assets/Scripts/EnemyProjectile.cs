using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    
    private GameObject _player;
    private Rigidbody2D rb;
    public float force;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = _player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;


        float rotation = Mathf.Atan2(-direction.y, -direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation+180);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            Destroy(gameObject);

        
        
    }
}
