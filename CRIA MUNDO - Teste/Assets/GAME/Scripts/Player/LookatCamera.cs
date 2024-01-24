using Photon.Pun;
using UnityEngine;

public class LookatCamera : MonoBehaviourPunCallbacks
{
    public Camera cameraToLookAt;

    public GameObject playerStatus;

    private void Start()
    {
        if (!photonView.IsMine) { return; }
        playerStatus.SetActive(false);
    }

    void Update()
    {
        if(photonView.IsMine) { return;}

        Camera cameraToLookAt = Camera.main;
        transform.LookAt(cameraToLookAt.transform);
    }

}