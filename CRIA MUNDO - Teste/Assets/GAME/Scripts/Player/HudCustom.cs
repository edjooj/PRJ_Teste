using Photon.Pun;
using UnityEngine;

public class HudCustom : MonoBehaviourPunCallbacks
{

    public GameObject hudCelular;
    public GameObject outroObjeto;

    void Start()
    {
       
        hudCelular.SetActive(false);
        
        outroObjeto.SetActive(true);
    }

    public void AbrirHud()
    {
        if (!photonView.IsMine) {return;}
        hudCelular.SetActive(true);
        
        outroObjeto.SetActive(false);
    }

    public void FecharHud()
    {
        if (!photonView.IsMine) { return; }
        hudCelular.SetActive(false);
        
        outroObjeto.SetActive(true);
    }
}




