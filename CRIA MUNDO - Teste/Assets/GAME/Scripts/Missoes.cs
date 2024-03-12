using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Missoes : MonoBehaviourPunCallbacks
{
    public Slider barra;
    public TextMeshProUGUI porcent;
    public float valorMax = 1000f;
    private float currentValue = 0f;
    public Button button;
    private float porcentagem = 0f;


   
    public void Start()
    {
        barra.maxValue = valorMax;
        barra.value = currentValue;

    }

   

    public void BarraUpdate()
    {

        currentValue += 32;
        Debug.Log(currentValue);
        porcentagem = currentValue / 10;
        if(currentValue >= valorMax)
        {
            porcentagem = 100f;
        }
        somandoBar();
        porcent.text = string.Format("{0}%", porcentagem);
    }

    [PunRPC]
    public void somandoBar()
    {
        
        barra.value = currentValue;

        if(currentValue >= valorMax)
        {
            Debug.Log("Esta cheio");
        }

    }   

   

    public void ValueChangeCheck()
    {
        
        barra.value += 1f;
        Debug.Log(barra.value);
    }

   

}
