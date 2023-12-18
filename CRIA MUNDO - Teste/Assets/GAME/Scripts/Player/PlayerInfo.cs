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

    [PunRPC]
    public void UpdatePlayerCustomization(int camisaId, int cabeloId, int calcaId, int chapeuId, int sapatoId)
    {
        if (photonView.IsMine)
        {
            // Aplique as customizações apenas ao jogador que possui este PhotonView
            NetworkController.instance.customize.camisa = camisaId;
            NetworkController.instance.customize.cabelo = cabeloId;
            NetworkController.instance.customize.calca = calcaId;
            NetworkController.instance.customize.chapeu = chapeuId;
            NetworkController.instance.customize.sapato = sapatoId;

            // Atualize a customização visual
            NetworkController.instance.customize.customize.MeshSelect();
        }
    }
}

