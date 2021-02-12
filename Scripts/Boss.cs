using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;
    public float spawnOffset;

    private int halfHealth;
    private Animator anim;
    public bool Bossdead;
    public int damage;

    public GameObject deathEffect;
    public GameObject deathSplat;
    public WaveSpawner WaveSpawnerinfo;
    public int WaveNumber;
    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        WaveSpawnerinfo = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>();
        WaveNumber = WaveSpawnerinfo.WaveNumber;
    }
    public void TakeDamage(int damageAmount)
    {

        health -= damageAmount;
   
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(deathSplat, transform.position, Quaternion.identity);
            gameObject.transform.Translate(-100, 100, -200);
            Bossdead = true;
            if (Bossdead == true)
            {
                Debug.Log("Game Finished!!!");
                WaveNumber = 100;
            }
        }
        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
        FindObjectOfType<AudioManager>().Play("BossDamage");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
