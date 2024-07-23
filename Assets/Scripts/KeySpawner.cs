using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject key;
    public GameObject character;
    public GameObject levelUp;
    public int counterSlime = 0;
    public int counterMurcielago = 0;
    public int counterEsqueleto = 0;
    bool expertMode = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dificil") 
        {
            expertMode = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (expertMode == false) //modo clasico
        {
            if (collision.gameObject.tag == "Slime")
            {
                counterSlime++;
                if (counterSlime == 14)
                {
                    levelUp.GetComponent<LevelUp>().level = 2;
                    character.GetComponent<Character>().specialAttackDamage += 5;
                    GameObject newobject = Instantiate(key, transform.position, Quaternion.identity);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            if (collision.gameObject.tag == "Murcielago")
            {
                counterMurcielago++;
                if (counterMurcielago == 8)
                {
                    levelUp.GetComponent<LevelUp>().level = 3;
                    character.GetComponent<Character>().armor = 5;
                    GameObject newobject = Instantiate(key, transform.position, Quaternion.identity);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            if (collision.gameObject.tag == "Esqueleto")
            {
                counterEsqueleto++;
                if (counterEsqueleto == 8)
                {
                    levelUp.GetComponent<LevelUp>().level = 4;
                    character.GetComponent<Character>().attackDamage += 5;
                    GameObject newobject = Instantiate(key, transform.position, Quaternion.identity);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        else //modo experto
        {
            if (collision.gameObject.tag == "Slime")
            {
                counterSlime++;
                if (counterSlime == 20)
                {
                    levelUp.GetComponent<LevelUp>().level = 2;
                    character.GetComponent<Character>().specialAttackDamage += 5;
                    GameObject newobject = Instantiate(key, transform.position, Quaternion.identity);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            if (collision.gameObject.tag == "Murcielago")
            {
                counterMurcielago++;
                if (counterMurcielago == 10)
                {
                    levelUp.GetComponent<LevelUp>().level = 3;
                    character.GetComponent<Character>().armor = 5;
                    GameObject newobject = Instantiate(key, transform.position, Quaternion.identity);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            if (collision.gameObject.tag == "Esqueleto")
            {
                counterEsqueleto++;
                if (counterEsqueleto == 10)
                {
                    levelUp.GetComponent<LevelUp>().level = 4;
                    character.GetComponent<Character>().attackDamage += 5;
                    GameObject newobject = Instantiate(key, transform.position, Quaternion.identity);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
       
    }
}
