using UnityEngine;
using Photon.Pun;

public class CameraSwitch : MonoBehaviourPunCallbacks
{
    private Camera MainCamera;
    public Camera minigameCamera;

    void Start()
    {
        Camera[] cameras = GetComponentsInChildren<Camera>();
    }

    public void ActivateMinigameCamera()
    {
        MainCamera.enabled = false;
        minigameCamera.enabled = true;
    }

    public void DeactivateMinigameCamera()
    {
        MainCamera.enabled = true;
        minigameCamera.enabled = false;
    }
}
