using Photon.Pun;
using UnityEngine;

public class MobileControl : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject mobilePanel;

    /*private void Start()
    {
        if (!photonView.IsMine) { return; }
        mobilePanel = GameObject.FindGameObjectWithTag("MobileController");


            if (!Application.isMobilePlatform)
            {

                    mobilePanel.SetActive(false);
            }
    }*/
}
