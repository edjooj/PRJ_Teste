using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerCameraController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject targetPlayerCamera;
    [SerializeField] private new GameObject camera;
    [SerializeField] private Camera targetCamera;
    [SerializeField] private CinemachineFreeLook vcam;


    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!photonView.IsMine) { return; }
        camera.SetActive(true);
        vcam = FindAnyObjectByType<CinemachineFreeLook>();
        vcam.Follow = targetPlayerCamera.transform;
        vcam.LookAt = targetPlayerCamera.transform;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
