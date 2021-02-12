    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float StopDistance;

    private float AttackTime;
    
    public float AttackSpeed;
    private void Update()
    {
        if (player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > StopDistance) //if enemy is too far away from player
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else // enemy is near enough to attack main character
            {
                if (Time.time >= AttackTime)
                {
                    StartCoroutine(Attack());
                    //attack
                    AttackTime = Time.time + TimeBetweenAttacks;
                }
            }
        }
    }
    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage); //amount of damage the monster inflicts on character
        Vector2 originalPosition = transform.position; // monster position before he leaps to player
        Vector2 targerPosition = player.position; // where the monster will leap to

        float percent = 0;
        while(percent <= 1)
        {
            percent += Time.deltaTime * AttackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;  //time it takes for next attack
            transform.position = Vector2.Lerp(originalPosition, targerPosition, formula);
            yield return null;
        }
    }
}
