﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    private Player playerScript;
    private Vector2 targetPosition;
    public float speed;
    public int damage;
    public GameObject effect;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > .1f) //move the fireball when shot by mini dragon
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
           
        }
        else
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            Destroy(gameObject); //once fireball hits player
        }
    }
}
