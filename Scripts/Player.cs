using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    private Animator anim;

    private Vector2 MoveAmount;
    public bool GunEquipped;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int health;
    Animator cameraAnim;

    public Animator hurtAnim;
     public Transform PlayerTransform;
    public bool PlayerDead;
 
   private bool GunPickup;
    private void Start()
    {
        PlayerTransform = gameObject.transform;
        cameraAnim = Camera.main.GetComponent<Animator>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update() // updates per frame
    {
        Movement();
        if (GunPickup)
            print("b");
    }

    private void Movement()
    {
        Vector2 MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // gets input from user i.e up,down etc.
        MoveAmount = MoveInput.normalized * speed; // Movement of player is the move input * how fast a player is

        if (MoveInput != Vector2.zero)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false); //tells the animator if the player is moving or not
        }
    }

    private void FixedUpdate() // for any frames that involve physics
    {
        rb.MovePosition(rb.position + MoveAmount * Time.fixedDeltaTime);//So the speed is the same no matter how powerful the computer
    }
    public void TakeDamage(int damageAmount)
    {
        cameraAnim.SetTrigger("shake");
        health -= damageAmount;
        UpdateHealthUI(health);
      
        FindObjectOfType<AudioManager>().Play("PlayerDamage");
        hurtAnim.SetTrigger("hurt");
        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Destroy(gameObject);
            UpdateHealthUI(health);

        }

    }


    public void ChangeWeapon(Weapon weaponToEquip)
    {
        //if (GunEquipped == false)
        //{
        //    GameObject.FindGameObjectWithTag("Gun").SetActive(false);
        //}
        Destroy(GameObject.FindGameObjectWithTag("Weapon")); // destroy players current weapon
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform); // instantiate the weapon to equip to the player position, rotation
        GunEquipped = false;
    
    }
    //public void TwoGuns(Weapon GunToEquip)
    //{
    //    if (GunEquipped)
    //    {
    //        Instantiate(GunToEquip, transform.position, transform.rotation, transform); // instantiate the weapon to equip to the player position, rotation
    //    }
        
    //}

    void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++) // runs for every heart the player has
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart; // adds in full heart sprite
            }
            else
            {
                hearts[i].sprite = emptyHeart; // adds in empty heart sprite
            }
        }
    }
    public void Heal(int healAmount)
    {
        if (health + healAmount > 10) // check if player health is already at 5 if so then do nothing
        {
            health = 10;
        }
        else
        {
            health += healAmount; // adds one health to players current health
        }
        UpdateHealthUI(health); //updates the health bar
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Gun")
        {
            GunPickup = true;
        }
    }
}
