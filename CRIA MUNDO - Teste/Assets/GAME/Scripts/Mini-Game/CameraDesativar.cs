using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraDesativar : MonoBehaviourPunCallbacks
{
    private GameObject camUsuario;

    private void Start()
    {
      
        camUsuario = GameObject.FindGameObjectWithTag("MainCamera");

        
        
    }

    public void Desligar()
    {
      
        if (camUsuario != null)
        {
            camUsuario.SetActive(false);
        }
    }

    public void Ligar()
    {
        
        if (camUsuario != null)
        {
            camUsuario.SetActive(true);
          
        }
    }
}
