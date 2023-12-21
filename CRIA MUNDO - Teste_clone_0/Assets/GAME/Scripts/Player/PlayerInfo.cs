using Photon.Pun;
using UnityEngine;
using TMPro;
using Firebase.Auth;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public TMP_Text nicknameText;
    private FirebaseAuth auth;

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

    [PunRPC]
    public void UpdateNickname(string nickname)
    {
        nicknameText.text = nickname;
    }
}

