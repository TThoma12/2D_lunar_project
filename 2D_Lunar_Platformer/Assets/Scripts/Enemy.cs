
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float Health=2;
    public float Speed;
    public int damage;
    private float timerToShoot;

    public Transform bulletPosition;
     GameObject Player;
    NavMeshAgent agent;

    Collider2D _ObjectCollider;
    public GameObject ProjectilePrefab;

    private void Start()
    {
        _ObjectCollider = GetComponent<Collider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        agent.SetDestination(Player.gameObject.transform.position) ;

        timerToShoot += Time.deltaTime;
        if(timerToShoot > 2)
        {
            _ObjectCollider.isTrigger = true;
            timerToShoot = 0;
            EnemyShoot();
            Invoke("UnTrigger", 0.3f);
        }
       
    }

    private void EnemyShoot()
    {
        Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health == 0) {
            Destroy(gameObject);         
        }
    }
    public void UnTrigger()
    {
        _ObjectCollider.isTrigger = false;
    }
}
