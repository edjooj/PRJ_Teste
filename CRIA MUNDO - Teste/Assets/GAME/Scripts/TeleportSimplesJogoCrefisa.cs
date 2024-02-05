using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportSimplesJogoCrefisa : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform spawnPosition;

    [SerializeField] GameObject jogoCrefisa;





    private void Awake()
    {
        //GameObject jogoCrefisa = GameObject.FindWithTag("GameCrefisa");
        GameObject spawnPointObject = GameObject.FindWithTag("SpawnPointCrefisa");

        if (spawnPointObject != null)
        {
            spawnPosition = spawnPointObject.transform;
        }
        else
        {
            Debug.LogError("SpawnPoint GameObject não encontrado com a tag 'SpawnPoint'.");
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
                    Destroy(jogoCrefisa);
                }
               
            }
            else
            {
                Debug.LogError("Player GameObject not found.");
            }
        }
    }

   
}
