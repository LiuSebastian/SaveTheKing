using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour
{
    Animator anim;
    public GameObject potion;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Roto", false);
    }
 public void ChangeAnimation()
    {
        anim.SetBool("Roto", true);
        if (potion != null)
        {
            GameObject newobject = Instantiate(potion, transform.position, Quaternion.identity);
        }        
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 8);
    }
}
