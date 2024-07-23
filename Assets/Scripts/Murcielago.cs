using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murcielago : MonoBehaviour
{
    
    Animator anim;
    SpriteRenderer sp;
    public Transform target;
    public GameObject door;

    int enemyMaxHealth = 40;
    int enemyDamage = 15;
    public int enemyCurrentHealth;

    Vector3 direction;
    int speed = 2;
    bool attackAnim;
    bool wasHit = false;
    float wasHitCd = 0;
    bool notInTheRoom = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        enemyCurrentHealth = enemyMaxHealth;
    }


    void Update()
    {
        if (attackAnim == false && wasHit == false && notInTheRoom == false)
        {
            direction = target.position - transform.position;
            transform.position += (Time.deltaTime * direction.normalized * speed);
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
        if (enemyCurrentHealth > enemyMaxHealth)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }        
    }
    private void OnTriggerStay2D(Collider2D collision) //modo experto
    {
        if (collision.gameObject.tag == "Dificil")
        {
            enemyDamage = 20;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Ataque");
            attackAnim = true;
            collision.GetComponent<Character>().TakeDamage(enemyDamage);
            enemyCurrentHealth += 10;
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
   // public void TakeDamage(int damage)
    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        wasHit = true;
        anim.SetTrigger("Recibe Daño");
        if (enemyCurrentHealth <= 0)
        {
            anim.SetBool("Muerte", true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
            Destroy(gameObject, 8);
        }
    }

    public void Die()
    {
        anim.SetBool("Muerte", true); 
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 8);
    }
}
