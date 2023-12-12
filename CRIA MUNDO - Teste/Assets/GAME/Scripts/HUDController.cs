using TMPro;
using UnityEngine;
using Photon.Pun;

public class HUDController : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerCoin;

    private void Update()
    {
            playerCoin.text = CORE.instance.status.playerCoin.ToString();
    }
}
