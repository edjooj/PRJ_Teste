using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] string sceneName;
    [SerializeField] TeleportName[] scenes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PhotonView photonView = other.gameObject.GetComponent<PhotonView>();
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            if (photonView != null && photonView.IsMine)
            {
                controller.enabled = false;
                SceneTeleport();

                other.gameObject.transform.position = spawnPosition.position;
                other.gameObject.transform.rotation = spawnPosition.rotation;
                controller.enabled = true;
            }
        }
    }


    void SceneTeleport()
    {


        foreach (TeleportName teleportName in scenes)
        {
            if (teleportName.sceneName != sceneName)
            {
                teleportName.gameObject.SetActive(false);
            }
            else
            {
                teleportName.gameObject.SetActive(true);
            }
        }
    }

}
