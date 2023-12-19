using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class PlayerCameraController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject targetPlayerCamera;
    [SerializeField] private new GameObject camera;
    [SerializeField] private Camera targetCamera;
    [SerializeField] private CinemachineFreeLook vcam;

        private void Start()
        {
            if (!photonView.IsMine) { return; }
            camera.SetActive(true);
            vcam = FindAnyObjectByType<CinemachineFreeLook>();
            vcam.Follow = targetPlayerCamera.transform;
            vcam.LookAt = targetPlayerCamera.transform;
        }

}
