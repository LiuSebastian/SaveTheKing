using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Esqueleto : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;
    Animator anim;
    public AudioSource sonido;
    public Transform target;

    int enemyMaxHealth = 70;
    int enemyDamage = 20;
    public int enemyCurrentHealth;  

    Vector3 direction;
    int speed = 2;
    bool attackAnim;
    bool wasHit = false;
    float wasHitCd = 0;
    public bool notInTheRoom = true;
    
    void Start()
    {        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();      
        sp = GetComponent<SpriteRenderer>();

        enemyCurrentHealth = enemyMaxHealth;       
    }


    void Update()
    {
        if (attackAnim == false && wasHit == false && notInTheRoom == false)
        {
            anim.SetBool("Stay", false);
            direction = target.position - transform.position;
            transform.position += (Time.deltaTime * direction.normalized * speed);
        }
        else
        {
            anim.SetBool("Stay", true);
        }
        if (wasHit == true)
        {
            wasHitCd += 1 * Time.deltaTime;
        }
        if (wasHitCd >= 2)
        {
            wasHitCd = 0;
            wasHit = false;
        }
        
        if (direction.x > 0)
        {
            sp.flipX = false;
        }
        if (direction.x < 0)
        {
            sp.flipX = true;
        }               
    }
    private void OnTriggerStay2D(Collider2D collision) //modo experto
    {
        if (collision.gameObject.tag == "Dificil")
        {
            enemyDamage = 25;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            anim.SetTrigger("Attack");
            attackAnim = true;
            collision.GetComponent<Character>().TakeDamage(enemyDamage);          
        }       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackAnim = false;
        }
        if (collision.gameObject.tag == "Paredes")
        {
            notInTheRoom = false;
        }
    }

    public void TakeDamage(int damage) 
    {
        enemyCurrentHealth -= damage;
        wasHit = true;
        anim.SetTrigger("Hit");
        Debug.Log("ouch");
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die() 
    {
        anim.SetBool("Dead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 8);       
    }
}
