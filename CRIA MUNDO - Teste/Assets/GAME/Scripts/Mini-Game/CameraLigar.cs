using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CameraLigar : MonoBehaviourPunCallbacks
{
    public GameObject camUsuario;

    public void LigarCamera()
    {

        if (camUsuario != null)
        {
            camUsuario.SetActive(true);
            Debug.Log("Camera do player ligada.");
        }
       
    }
    
    public void DesligarCamera()
    {
        if (camUsuario != null)
        {
            camUsuario.SetActive(false);
            Debug.Log("Camera do player desligada.");
        }
       
    }
}
