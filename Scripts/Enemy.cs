using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    [HideInInspector]// to hide in unity inspector
    public Transform player;

    public float speed;

    public float TimeBetweenAttacks;

    public int damage;

    public int itempickupChance;
    public GameObject[] itempickups;

    public int healthPickupChance;
    public GameObject healthPickup;

    public GameObject deathEffect;
    public GameObject deathSplat;
    public int deathsplattime;
    private GameObject enemydeath;

    public virtual void Start()
    {
        enemydeath = GameObject.FindWithTag("deathSplat");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("SplatTime", deathsplattime);
    }


    void SplatTime()
    {
        Destroy(enemydeath);
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < itempickupChance)
            {
                GameObject itemrandomPickup = itempickups[Random.Range(0, itempickups.Length)]; // chance of random pickup
                Instantiate(itemrandomPickup, transform.position, transform.rotation); // spawns the random weapon in position of dead monster
            }
            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {

                Instantiate(healthPickup, transform.position, transform.rotation);
                FindObjectOfType<AudioManager>().Play("ThatsCrazy"); //death sound

            }
            Instantiate(deathEffect, transform.position, Quaternion.identity); // to put death effect on enemy
            Instantiate(deathSplat, transform.position, Quaternion.identity);


            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Death");
        }
    }
    
        
        

}
