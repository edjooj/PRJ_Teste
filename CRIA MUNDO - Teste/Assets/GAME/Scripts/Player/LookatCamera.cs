using Photon.Pun;
using UnityEngine;

public class LookatCamera : MonoBehaviourPunCallbacks
{
   // private Camera cameraToLookAt;
    public Transform playerStatus;

    void Update()
    {
        OrientarCabecaParaJogador();
    } 

    private void OrientarCabecaParaJogador()
    {
      if (playerStatus != null)
    {
            Vector3 direcaoDoJogador = (Camera.main.transform.position - playerStatus.position).normalized;
            playerStatus.LookAt(playerStatus.position + new Vector3(direcaoDoJogador.x, 0f, direcaoDoJogador.z));
    }
    }
}
