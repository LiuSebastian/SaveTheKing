using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaFlechas : MonoBehaviour
{
    public GameObject arrow;
    float cd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (cd >= 2)
            {
            GameObject newobject = Instantiate(arrow, transform.position, Quaternion.identity);
                cd = 0;
            }
        }
    }
    private void Update()
    {
        cd += 1 * Time.deltaTime;
    }
}
