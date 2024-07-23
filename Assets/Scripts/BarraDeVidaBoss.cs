using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaBoss : MonoBehaviour
{
    public Slider sliderVidaBoss;
    public GameObject boss;
    void Update()
    {
        sliderVidaBoss.value = boss.GetComponent<Boss>().enemyCurrentHealth;
    }
}
