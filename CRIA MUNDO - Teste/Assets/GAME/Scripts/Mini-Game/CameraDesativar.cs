using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraDesativar : MonoBehaviourPunCallbacks
{
    public void Ligar()
    {
        GameObject camUsuario = GameObject.FindWithTag("CameraPlayer");
        camUsuario.SetActive(true);
        
    }

    public void Desligar()
    {
        GameObject camUsuario = GameObject.FindWithTag("CameraPlayer");
        camUsuario.SetActive(false);
    }
}
