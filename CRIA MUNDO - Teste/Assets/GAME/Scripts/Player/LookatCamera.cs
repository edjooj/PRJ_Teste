using Photon.Pun;
using UnityEngine;

public class LookatCamera : MonoBehaviourPunCallbacks
{
    private Camera cameraToLookAt;
    public GameObject playerStatus;

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerStatus.SetActive(false);
        }
        else
        {
            cameraToLookAt = Camera.main;
        }
    }

    void Update()
    {
        if (photonView.IsMine) { return; }
        if (cameraToLookAt != null)
        {
            transform.LookAt(cameraToLookAt.transform);
        }
    }
}
