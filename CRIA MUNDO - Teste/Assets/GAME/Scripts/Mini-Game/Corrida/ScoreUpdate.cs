using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    public TMP_Text score;
    GameObject crefisaSpawn;

    private void OnEnable()
    {
        score.text = NetworkController.instance.scoreController.currentScorePoint.ToString();
        crefisaSpawn = GameObject.FindGameObjectWithTag("CrefisaSpawn");
    }

    public void ReturnToCity()
    {
        Transform returnTransform = crefisaSpawn.transform;

        this.gameObject.SetActive(false);

        PhotonView photonView = this.gameObject.GetComponent<PhotonView>();
        CharacterController controller = this.gameObject.GetComponent<CharacterController>();
        if (photonView != null && photonView.IsMine)
        {
            controller.enabled = false;

            this.gameObject.transform.position = returnTransform.position;
            this.gameObject.transform.rotation = returnTransform.rotation;
            controller.enabled = true;
        }

        PlayerVisibilityManager.ShowAllPlayers();

    }
}
