using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float MaxX;
    public float MinX;
    public float MaxY;
    public float MinY;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;
    public override void Start() // overide the start function in enemy class 
    {
        base.Start();
        float randomX = Random.Range(MinX, MaxX);
        float randomY = Random.Range(MinY, MaxX); // to choose the summoners spot to summon in the map
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition) > .5f) // if the summoner is far from the summoning spot make him run to that spot
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false); // changes animation of summoner to idle
                
                if (Time.time >= summonTime) // if summon time is less than actual time of game
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("Summon");
                 
                }

            }
        }
    }
    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("Witch");
        }
    }
}
