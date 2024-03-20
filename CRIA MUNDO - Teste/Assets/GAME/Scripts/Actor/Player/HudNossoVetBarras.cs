using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudNossoVetBarras : MonoBehaviourPunCallbacks
{
    public float bar1 = 5f;
    public float bar2 = 5f;
    public float bar3 = 5f;

    public Button btt1;
    public Button btt2;
    public Button btt3;

    public Image barraCarinho;
    public Image barraDoente;
    public Image barraFome;

    private void Start()
    {
        btt1.onClick.AddListener(IncrementBar1);
        btt2.onClick.AddListener(IncrementBar2);
        btt3.onClick.AddListener(IncrementBar3);
    }

    private void Update()
    {
        if(bar1 > 0)
        {
           
            bar1 -= Time.deltaTime;
            barraCarinho.fillAmount = bar1 / 110f;
        }

        if(bar2 > 0)
        {
            bar2 -= Time.deltaTime;
            barraDoente.fillAmount = bar2 /110f;
        }

        if(bar3 > 0)
        {
            bar3 -= Time.deltaTime;
            barraFome.fillAmount = bar3 / 110f;
        }
    }


    public void IncrementBar1()
    {
        bar1 += 5f;
        barraCarinho.fillAmount = bar1 * 60f;
    }

    public void IncrementBar2()
    {
        bar2 += 5f;
        barraDoente.fillAmount = bar2 * 60f;
        
    }

    public void IncrementBar3()
    {
        bar3 += 5f;
        barraFome.fillAmount = bar3 * 60f;
    }

}
