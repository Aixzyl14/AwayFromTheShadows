using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon WeaponToEquip;
    public GameObject effect;

    private void Start()
    {
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            //if (GameObject.FindGameObjectWithTag("Gun"))
            //{
            //    collision.GetComponent<Player>().ChangeWeapon(WeaponToEquip);
            //    Destroy(gameObject);
            //}
       
            
                collision.GetComponent<Player>().ChangeWeapon(WeaponToEquip);
                Destroy(gameObject);
                Instantiate(effect, transform.position, Quaternion.identity);
            
        }
    }
}
