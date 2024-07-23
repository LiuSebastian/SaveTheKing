using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderVida;  
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        sliderVida.value = player.GetComponent<Character>().playerCurrentHealth;       
    }
}
