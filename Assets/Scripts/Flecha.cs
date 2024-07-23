using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float speed;
    float cd;
    void Update()
    {
        transform.position += transform.up * -speed * Time.deltaTime;
        cd += 1 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Paredes")
        {
            if (cd > 0.3f)
            {
                Destroy(gameObject);
            }
        }
    }
}
