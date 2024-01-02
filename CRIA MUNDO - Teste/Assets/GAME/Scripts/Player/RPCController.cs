using Photon.Pun;
using UnityEngine;

public class RPCController : MonoBehaviourPunCallbacks
{
    [PunRPC]
    void HideOtherPlayers()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (!player.GetComponent<PhotonView>().IsMine)
            {
                player.SetActive(false);
            }
        }
    }
}
