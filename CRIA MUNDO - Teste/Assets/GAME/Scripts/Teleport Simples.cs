using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleporteSimples : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform spawnPosition;
    
    
    [SerializeField] GameObject hudTeleport;
    
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            hudTeleport.SetActive(true);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hudTeleport.SetActive(false);
        }
    }

    public void teleport()
    {
      //if (photonView != null && photonView.IsMine)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");


            if (playerObject != null)
            {
                CharacterController controller = playerObject.GetComponent<CharacterController>();

                if (controller != null)
                {
                    controller.enabled = false;
                    
                    playerObject.transform.position = spawnPosition.position;
                    playerObject.transform.rotation = spawnPosition.rotation;
                    controller.enabled = true;
                    Debug.Log("Player Teleportado");
                }
               
            }
            else
            {
                Debug.LogError("Player GameObject not found.");
            }
        }
    }

   
}
