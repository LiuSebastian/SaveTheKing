using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    SpriteRenderer sp;
    Animator anim;
    public AudioSource sonido;
    public Transform target;
    public GameObject door;
    int enemyMaxHealth = 20;
    int enemyDamage = 10;
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
            sonido.Play();
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
            enemyDamage = 15;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Attack");
            attackAnim = true;
            Debug.Log("ouch jeje");
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
        if (enemyCurrentHealth <= 0)
        {
            Die();           
        }
    }

    public void Die()
    {
        anim.SetBool("Dead", true);
        GetComponent<AudioSource>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 8);
    }
}