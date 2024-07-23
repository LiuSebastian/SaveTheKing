using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    SpriteRenderer sp;
    Animator anim;
    public AudioSource explotionSound;
    public AudioSource swordSlashSound;
    public Transform target;
    public GameObject player;
    public GameObject explotion;
    public Transform specialAttackPoint;
    public GameObject barraDeVida;
    public GameObject tpBoss;
    public GameObject tpPj;
    public LayerMask playerLayer;

    int enemyMaxHealth = 200;
    public int enemyDamage = 35;
    public int enemySpecialAttack = 100;
    public int enemyCurrentHealth;

    Vector3 direction;
    int speed = 2;
    bool attackAnim;
    public bool notInTheRoom = true;
    bool rageMode = false;

    public bool specialAttack = false;
    public int specialAttackRandom = 0;
    public bool specialAttackExplotion = false;
    float waitForRecover = 0;
    public float attackRange = 0.5f;
    float attackCd = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        enemyCurrentHealth = enemyMaxHealth;
    }
    void Update()
    {
        if (attackAnim == false && notInTheRoom == false)
        {
            anim.SetBool("Stay", false);
            direction = target.position - transform.position;
            transform.position += (Time.deltaTime * direction.normalized * speed);
        }
        else
        {
            anim.SetBool("Stay", true);
        }
        if (direction.x > 0)
        {
            sp.flipX = false;
        }
        if (direction.x < 0)
        {
            sp.flipX = true;
        }
        if (sp.color == Color.red)
        {
            waitForRecover += 1 * Time.deltaTime;
            if (waitForRecover >= 1.30)
            {
                sp.color = Color.white;
                waitForRecover = 0;
            }
        }
        if (attackAnim == false)
        {
            specialAttackExplotion = false;
            attackCd += 1 * Time.deltaTime;
        }
        
        if (enemyCurrentHealth <= 100 && rageMode == false)
        {
            transform.position = tpBoss.transform.position;
            player.transform.position = tpPj.transform.position;
            rageMode = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && attackCd >= 3)
        {
            attackCd = 0;
            attackAnim = true;
            specialAttackRandom = Random.Range(1, 11);
            if (specialAttackRandom <= 3)
            {
                explotion.GetComponent<Explotion>().enabled = true;
                explotionSound.Play();
                anim.SetTrigger("Special Attack");
                attackAnim = true;                      
            }
            else
            {
                anim.SetTrigger("Attack");
                swordSlashSound.Play();
                collision.GetComponent<Character>().TakeDamage(enemyDamage);
                attackAnim = true;
            }           
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dificil")
        {
            enemyDamage = 45;
        }
        if (collision.gameObject.tag == "Furia")
        {
            enemyDamage = 55;
            explotion.GetComponent<Explotion>().explotionDamage = 75;
        }
        if (specialAttackExplotion == true)
        {           
            player.GetComponent<Character>().TakeDamage(enemySpecialAttack);
            specialAttack = false;
            attackAnim = true;
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
            barraDeVida.GetComponent<BarraDeVidaBoss>().gameObject.SetActive(true);
        }
    }
    public void TakeDamage(int damage)
    {
        sp.color = Color.red;
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            Die();
        }      
    }
    public void Die()
    {
        anim.SetBool("Dead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 8);
        SceneManager.LoadScene("Victoria");
    }
    private void OnDrawGizmosSelected()
    {
        if (specialAttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(specialAttackPoint.position, attackRange);
    }
}
