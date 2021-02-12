using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint; // position of the projectile being shot
    public float timeBetweenShots;

    private float shotTime; // exact time the projectile is shot
    Animator cameraAnim;

    private void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>(); 
    }

    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position ; //current mouse position - current weapon positon
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // make the float 'angle' into a unity rotation(quaternion) what is used to rotate an object in unity
        transform.rotation = rotation; //transforms the weapon rotation to the rotation of the mouse

        if(Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, new Vector3(shotPoint.position.x, shotPoint.position.y, 0), transform.rotation);
                cameraAnim.SetTrigger("zoom");
                shotTime = Time.time + timeBetweenShots; // time the player needs to wait between each shot
            }

        }
    }
}
