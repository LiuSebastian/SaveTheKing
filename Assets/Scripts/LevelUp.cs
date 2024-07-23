using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    Animator anim;
    public int level;
    public float time = 0;
    void Start()
    {
        level = 1;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (level)
        {
            case 1:
                //invisible
                break;
            case 2:
                anim.SetTrigger("Special");
                time += 1 * Time.deltaTime;
                break;
            case 3:
                anim.SetTrigger("Armor");
                time += 1 * Time.deltaTime;
                break;
            default:
                anim.SetTrigger("Damage");
                time += 1 * Time.deltaTime;
                break;
        }
        if (time >= 5)
        {
            time = 0;
            level = 1;
        }
    }
}
