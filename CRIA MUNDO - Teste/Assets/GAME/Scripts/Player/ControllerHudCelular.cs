using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHudCelular : MonoBehaviour
{
    public GameObject HudCelular;
    
    void Start()
    {
        HudCelular.SetActive(false);
        
    }
    public void AbrirHud()
    {
        HudCelular.SetActive(true);
    }

    public void FecharHud()
    {
        HudCelular.SetActive(false);
    }

}
