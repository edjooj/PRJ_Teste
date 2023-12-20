using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FimDeFase : MonoBehaviour
{
    private CharacterController charactercontroller;
    public Cronometro cronometro;
    public GameObject endLevelHUD;
    private void Start()
    {
        charactercontroller = GetComponent<CharacterController>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            Debug.Log("FIM");

            endLevelHUD.SetActive(true);

            
            Time.timeScale = 0f;
        }
    }

     

    public void FimDaFase()
    {
        cronometro.PararCronometro();
        cronometro.SalvarTempoNoFinalDaFase(); 
    }

}
