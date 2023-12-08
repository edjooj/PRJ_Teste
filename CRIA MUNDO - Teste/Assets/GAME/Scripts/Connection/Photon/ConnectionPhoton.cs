using Photon.Pun;
using UnityEngine;

public class ConnectionPhoton : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public string userName;


    public void ConnectToMaster()
    {
        PhotonNetwork.NickName = userName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Voce entrou na sala" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GAME");
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }

    public void SetUserName(string name)
    {
        userName = name;
    }
}
