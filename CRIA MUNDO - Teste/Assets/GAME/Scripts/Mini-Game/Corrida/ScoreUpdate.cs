using Photon.Pun;
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

        PhotonView photonView = NetworkController.instance.player.GetComponent<PhotonView>();
        CharacterController controller = NetworkController.instance.player.GetComponent<CharacterController>();

        if (photonView != null && photonView.IsMine)
        {
            controller.enabled = false;

            NetworkController.instance.player.transform.position = returnTransform.position;
            NetworkController.instance.player.transform.rotation = returnTransform.rotation;
            controller.enabled = true;
        }

        PlayerVisibilityManager.ShowAllPlayers();

        this.gameObject.SetActive(false);
    }
}
