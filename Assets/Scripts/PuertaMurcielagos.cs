using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaMurcielagos : MonoBehaviour
{ 
    Animator anim;
    public GameObject key;
    public GameObject characterKeys;
    public AudioSource sonido;
    private void Start()
    {       
        anim = GetComponent<Animator>();
        anim.SetBool("Puerta Abierta", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() && characterKeys.GetComponent<Character>().keys == 1)
        {
            sonido.Play();
            anim.SetBool("Puerta Abierta", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
