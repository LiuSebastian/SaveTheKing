using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    SpriteRenderer sp;
    Animator anim;
    public AudioSource espadaso;
    public GameObject kingsDoor;

    public Transform attackPointLeft;
    public Transform attackPointRight;
    public Transform attackPointBack;
    public Transform attackPointFront;
    public Transform attackPointArround;
    public LayerMask enemyLayers;



    public float attackRange;
    public float attackAreaRange;

    public int attackDamage = 10; //esta pegando el doble del numero
    public int specialAttackDamage = 10;
    float attackRate = 1f;
    public float nextAttackTime = 0f;
    int attackNumber;
    public bool canAttack = false;
    
    public float  inputX;
    float inputY;
    public float speed;

    int playerMaxHealth = 100;
    int playerMaxEnergy = 50;
    public int armor = 0;
    public int playerCurrentEnergy;
    public int playerCurrentHealth;
    public int potions = 0;
    int potionHeal = 30;

    bool attacking;
    public bool defending = false;
    public bool invulnerable = false;
    public float invulnerableCd = 0;
    public bool wasHit = false;

    public int keys = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        playerCurrentHealth = playerMaxHealth;
    }

    void Update()
    {
        if (playerCurrentEnergy > playerMaxEnergy)
        {
            playerCurrentEnergy = playerMaxEnergy;
        }
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        nextAttackTime += 1 * Time.deltaTime;
        if (nextAttackTime >= attackRate)
        {           
            canAttack = true;
            if (attacking == true)
            {
                nextAttackTime = 0;
            }          
        }
        else
        {
            canAttack = false;
        }

        //Invulnerabilidad al recibir daño
        if (wasHit == true)
        {
            invulnerableCd += 1 * Time.deltaTime;
            invulnerable = true;
        }         
        if (invulnerableCd >= 2)
        {
            sp.color = Color.white;
            invulnerable = false;
            invulnerableCd = 0;
            wasHit = false;
        }
            
        //Movimiento
        if (defending == false)
        {
            transform.position += transform.right * inputX * speed * Time.deltaTime;
            transform.position += transform.up * inputY * speed * Time.deltaTime;
        }
        
        if (inputX > 0)
        {
            anim.SetFloat("Direction", 1);
            anim.SetBool("Stay", false);
           
        }
        if (inputX < 0)
        {
            anim.SetFloat("Direction", -1);
            anim.SetBool("Stay", false);
            
        }
        if (inputX == 0 && inputY == 0)
        {
            anim.SetBool("Stay", true);
            anim.SetFloat("Direction", 0);
        }
        if (inputX == 0 && inputY > 0 || inputX == 0 && inputY < 0)
        {
            anim.SetBool("Stay", false);
            anim.SetFloat("Direction", 1);
        }
        if (canAttack == true)
        {
            attacking = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            if (Input.GetKey(KeyCode.LeftShift))
            {

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    GetComponent<CapsuleCollider2D>().enabled = true;
                    defending = true;
                    anim.SetTrigger("Defend Right");
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    GetComponent<CapsuleCollider2D>().enabled = true;
                    defending = true;
                    anim.SetTrigger("Defend Left");
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    GetComponent<CapsuleCollider2D>().enabled = true;
                    defending = true;
                    anim.SetTrigger("Defend Back");
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    GetComponent<CapsuleCollider2D>().enabled = true;
                    defending = true;
                    anim.SetTrigger("Defend Front");
                }
            }
            //Ataque
            else
            {
                defending = false;
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    attackNumber = 1;
                    attacking = true;
                    anim.SetTrigger("Attack Right");
                    Attack();                   
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    attackNumber = 2;
                    attacking = true;
                    anim.SetTrigger("Attack Left");
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    attackNumber = 3;
                    attacking = true;
                    anim.SetTrigger("Attack Back");
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    attackNumber = 4;
                    attacking = true;
                    anim.SetTrigger("Attack Front");
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.F)) //Ataque especial
                {
                    if (playerCurrentEnergy >= 50)
                    {
                        attackNumber = 5;
                        attacking = true;
                        anim.SetTrigger("Attack Arround");
                        playerCurrentEnergy = 0;
                        Attack();
                    }                   
                }
                canAttack = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (potions > 0)
            {
                playerCurrentHealth += potionHeal;
                potions--;
                if (playerCurrentHealth > playerMaxHealth)
                {
                    playerCurrentHealth = playerMaxHealth;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Potion>())
        {
            potions++;
        }
        if (collision.GetComponent<Key>())
        {
            kingsDoor.GetComponent<PuertaDelRey>().keysCounter++;
        }
        if (collision.GetComponent<Key>())
        {
            keys++;
        }
        if (collision.gameObject.tag == "Flecha")
        {
            TakeDamage(15);
        }      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pinches")
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(int damage)
    {
        if (defending == false)
        {
            //si me pegaron hace menos de 2 segundos
            if (invulnerable == false)
            {
                sp.color = Color.red;
                wasHit = true;
                playerCurrentHealth -= (damage - armor);
                invulnerable = true;
            }
        }      
        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        anim.SetBool("Die", true);
        this.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        SceneManager.LoadScene("Lose");
    }

    void Attack()
    {
        espadaso.Play();
        switch (attackNumber)
        {           
            case 1:
                Collider2D[] hitEnemiesRight = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemiesRight)
                {
                    if (enemy.GetComponent<Esqueleto>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Esqueleto>().TakeDamage(attackDamage);                      
                    }
                    if (enemy.GetComponent<Murcielago>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Murcielago>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Slime>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Slime>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Barril>())
                    {
                        enemy.GetComponent<Barril>().ChangeAnimation();
                    }
                    if (enemy.GetComponent<Boss>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                    }
                }
                break;
            case 2:               
                Collider2D[] hitEnemiesLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemiesLeft)
                {
                    if (enemy.GetComponent<Esqueleto>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Esqueleto>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Murcielago>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Murcielago>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Slime>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Slime>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Barril>())
                    {
                        enemy.GetComponent<Barril>().ChangeAnimation();
                    }
                    if (enemy.GetComponent<Boss>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                    }
                }     
                break;
            case 3:
                Collider2D[] hitEnemiesBack = Physics2D.OverlapCircleAll(attackPointBack.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemiesBack)
                {
                    if (enemy.GetComponent<Esqueleto>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Esqueleto>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Murcielago>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Murcielago>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Slime>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Slime>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Barril>())
                    {
                        enemy.GetComponent<Barril>().ChangeAnimation();
                    }
                    if (enemy.GetComponent<Boss>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                    }
                }
                break;
            case 4:
                Collider2D[] hitEnemiesFront = Physics2D.OverlapCircleAll(attackPointFront.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemiesFront)
                {
                    if (enemy.GetComponent<Esqueleto>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Esqueleto>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Murcielago>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Murcielago>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Slime>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Slime>().TakeDamage(attackDamage);
                    }
                    if (enemy.GetComponent<Barril>())
                    {
                        enemy.GetComponent<Barril>().ChangeAnimation();
                    }
                    if (enemy.GetComponent<Boss>())
                    {
                        playerCurrentEnergy += 5;
                        enemy.GetComponent<Boss>().TakeDamage(attackDamage);
                    }
                }
                break;
            default:
                Collider2D[] hitEnemiesArround = Physics2D.OverlapCircleAll(attackPointArround.position, attackAreaRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemiesArround)
                {
                    if (enemy.GetComponent<Esqueleto>())
                    {                       
                        enemy.GetComponent<Esqueleto>().TakeDamage(specialAttackDamage);
                    }
                    if (enemy.GetComponent<Murcielago>())
                    {
                        enemy.GetComponent<Murcielago>().TakeDamage(specialAttackDamage);
                    }
                    if (enemy.GetComponent<Slime>())
                    {
                        enemy.GetComponent<Slime>().TakeDamage(specialAttackDamage);
                    }
                    if (enemy.GetComponent<Barril>())
                    {
                        enemy.GetComponent<Barril>().ChangeAnimation();
                    }
                    if (enemy.GetComponent<Boss>())
                    {
                        enemy.GetComponent<Boss>().TakeDamage(specialAttackDamage);
                    }
                }
                break;
        }       
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPointLeft == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        if (attackPointRight == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        if (attackPointBack == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointBack.position, attackRange);
        if (attackPointFront == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointFront.position, attackRange);
        if (attackPointArround == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPointArround.position, attackAreaRange);
    }
}