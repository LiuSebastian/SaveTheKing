using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPociones : MonoBehaviour
{
    public Text keysText;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keysText.text = player.GetComponent<Character>().potions.ToString();
    }
}
