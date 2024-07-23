using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeEnergia : MonoBehaviour
{
    public Slider sliderEnergia;
    public GameObject player;
    void Update()
    {        
        sliderEnergia.value = player.GetComponent<Character>().playerCurrentEnergy;
    }
}
