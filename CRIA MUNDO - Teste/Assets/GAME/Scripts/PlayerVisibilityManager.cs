using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibilityManager : MonoBehaviourPunCallbacks
{
    public static List<GameObject> hiddenPlayers = new List<GameObject>();

    public static void HidePlayer(GameObject playerToHide)
    {
        if (!hiddenPlayers.Contains(playerToHide))
        {
            hiddenPlayers.Add(playerToHide);
            playerToHide.SetActive(false);
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
