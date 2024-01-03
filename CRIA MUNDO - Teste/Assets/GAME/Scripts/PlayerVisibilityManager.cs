using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibilityManager : MonoBehaviourPunCallbacks
{
    public static List<GameObject> hiddenPlayers = new List<GameObject>();

    public static void HidePlayer(GameObject playerToHide)
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView pv = playerToHide.GetComponent<PhotonView>();
            if (pv != null && !pv.IsMine)
            {
                Debug.Log("Escondendo " + player.name);
                player.SetActive(false);
            }
            else
            {
                Debug.Log("Ignorando " + player.name + " porque é o jogador local ou não possui PhotonView");
            }
        }

    }

    public static void ShowAllPlayers()
    {
        foreach (GameObject player in hiddenPlayers)
        {
            if (player != null)
            {
                player.SetActive(true);
            }
        }
        hiddenPlayers.Clear();
    }
}
