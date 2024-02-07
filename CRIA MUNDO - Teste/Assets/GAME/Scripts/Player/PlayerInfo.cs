using Photon.Pun;
using UnityEngine;
using TMPro;
using Firebase.Auth;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public TMP_Text nicknameText;
    public TMP_Text playerCourse;
    private FirebaseAuth auth;


    public GameObject player;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        DontDestroyOnLoad(this.gameObject);

        if (photonView.IsMine && auth.CurrentUser != null)
        {
            string nickname = auth.CurrentUser.DisplayName ?? "Anônimo";

            photonView.Owner.NickName = nickname;
            nicknameText.text = nickname;


            photonView.RPC("UpdateNickname", RpcTarget.AllBuffered, nickname);
        }
    }

    private void Update()
    {
        if (NetworkController.instance.player == null && photonView.IsMine)
        {
            NetworkController.instance.player = player;
        }
    }

    [PunRPC]
    public void UpdateNickname(string nickname)
    {
        nicknameText.text = nickname;
    }
}

