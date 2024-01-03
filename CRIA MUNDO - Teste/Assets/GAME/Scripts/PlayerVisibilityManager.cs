using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibilityManager : MonoBehaviourPunCallbacks
{
    public static List<GameObject> hiddenPlayers = new List<GameObject>();

    public static void HideOtherPlayers()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView pv = player.GetComponent<PhotonView>();
            if (pv != null && !pv.IsMine)
            {
                Debug.Log("Escondendo " + player.name);
                hiddenPlayers.Add(player); 
                player.SetActive(false);
            }
        }
    }

    public static void ShowAllPlayers()
    {
        foreach (GameObject player in hiddenPlayers)
        {
            if (player != null)
            {
                Debug.Log("Mostrando " + player.name);
                player.SetActive(true);
            }
        }
        hiddenPlayers.Clear();
    }
}
