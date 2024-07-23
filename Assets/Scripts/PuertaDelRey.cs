using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaDelRey : MonoBehaviour
{
    Animator anim;
    public AudioSource sonido;
    public int keysCounter;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Abierto", false);
        keysCounter = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() && keysCounter == 3)
        {
            sonido.Play();
            anim.SetBool("Abierto", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sonido.Play();
        anim.SetBool("Abierto", false);
        GetComponent<BoxCollider2D>().enabled = true;
        keysCounter = 0;
    }
}
