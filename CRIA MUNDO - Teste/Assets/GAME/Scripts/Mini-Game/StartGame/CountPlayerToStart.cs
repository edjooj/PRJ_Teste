using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountPlayerToStart : MonoBehaviourPunCallbacks
{

    public int playerToStart;
    public int currentplayer;

    public bool onlineMinigame = false;

    public string minigameName;
    public Sprite minigameIcon;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject minigameHud;
    public TextMeshProUGUI countPlayer;
    public PhotonView otherPhotonView;

    public float countdownTime = 5f;
    public TextMeshProUGUI countdownDisplay;

    public float currentTime;
    private bool timerIsActive = false;

    private void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        countPlayer.text = currentplayer.ToString();

        if(currentplayer >= playerToStart)
        {
            timerIsActive = true;
        }
        else
        {
            timerIsActive = false;
        }

        if (timerIsActive == true)
        {
            currentTime -= Time.deltaTime;
        }

        if (countdownDisplay != null)
        {
            countdownDisplay.text = currentTime.ToString("F2");
        }

        if (currentTime <= 0)
        {
            timerIsActive = false;
            currentTime = 0;

            StartMinigame();
        }
    }

    private void StartMinigame()
    {
        if (!onlineMinigame && otherPhotonView.IsMine)
        {
            PhotonNetwork.LeaveRoom(this);
            PhotonNetwork.LeaveLobby();
            Debug.Log("Desconectado do lobby");
        }

        if(otherPhotonView != null && otherPhotonView.IsMine)
        {
            PhotonNetwork.LoadLevel(minigameName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        otherPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (other.gameObject.CompareTag("Player") && otherPhotonView != null && otherPhotonView.IsMine)
        {
            minigameHud.SetActive(true);
        }
            currentplayer++;

    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView otherPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (other.gameObject.CompareTag("Player") && otherPhotonView != null && otherPhotonView.IsMine)
        {
            minigameHud.SetActive(false);

        }
        currentplayer--;
        currentTime = countdownTime;
    }
}
