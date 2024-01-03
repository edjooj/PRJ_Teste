using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    public TMP_Text score;
    public Transform Return;

    private void OnEnable()
    {
        score.text = NetworkController.instance.scoreController.currentScorePoint.ToString();
    }

    public void ReturnToCity()
    {
        this.gameObject.SetActive(false);

        PhotonView photonView = this.gameObject.GetComponent<PhotonView>();
        CharacterController controller = this.gameObject.GetComponent<CharacterController>();
        if (photonView != null && photonView.IsMine)
        {
            controller.enabled = false;

            this.gameObject.transform.position = Return.position;
            this.gameObject.transform.rotation = Return.rotation;
            controller.enabled = true;
        }
    }
}
