using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    public GameObject player;
    float timeUntilDestroy = 0;
    public bool enable = false;
    public int explotionDamage = 50;
    public bool active = false;
    public GameObject expertMode;

    private void Start()
    {
        if (expertMode != null)
        {
            explotionDamage = 65;
        }
    }
    private void Update()
    {
            timeUntilDestroy += 1 * Time.deltaTime;
            if (timeUntilDestroy >= 1.30 && timeUntilDestroy < 4)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<CircleCollider2D>().enabled = true;           
            }
            if (timeUntilDestroy >= 4)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Explotion>().enabled = false;
                timeUntilDestroy = 0;
            }
        if (active == true)
        {
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>())
        {
            player.GetComponent<Character>().TakeDamage(explotionDamage);        
        }
    }
}
