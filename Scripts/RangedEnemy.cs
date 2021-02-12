using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;

    public float attackTime;

    private Animator anim;

    public Transform shotPoint;

    public GameObject EnemyFireball;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance) // if monster is too far from player to attack it will move towards player
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            if (Time.time >= attackTime)
            {
                attackTime = Time.time + TimeBetweenAttacks;
                anim.SetTrigger("attack");
            }
        }
    }
    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // make the float 'angle' into a unity rotation(quaternion) what is used to rotate an object in unity
        shotPoint.rotation = rotation;

        Instantiate(EnemyFireball, shotPoint.position, shotPoint.rotation);
    }
}
