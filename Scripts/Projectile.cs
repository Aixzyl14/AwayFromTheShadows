using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float lifeTime;

    public GameObject explosion;

    public int damage;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        FindObjectOfType<AudioManager>().Play("Fireball");
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); //initiate the explosion at the certain point and have its rotation correct
       
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }

        if (collision.tag == "boss")
        {
            collision.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
