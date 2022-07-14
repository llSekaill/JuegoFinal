using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaBoss : MonoBehaviour
{
    private Slider sliderBoss;


    private void Start()
    {
        sliderBoss = GetComponent<Slider>();
    }

    public void CambiarVidaMaximaBoss(float vidaMaximaBoss)
    {
        sliderBoss.maxValue = vidaMaximaBoss;

    }
    
    public void CambiarVidaActualBoss(float cantidadVidaBoss)
    {
        sliderBoss.value = cantidadVidaBoss;

    }

    public void InicializarBarraDeVidaBoss(float cantidadVidaBoss)
    {
        CambiarVidaMaximaBoss(cantidadVidaBoss);
        CambiarVidaActualBoss(cantidadVidaBoss);
    }

}