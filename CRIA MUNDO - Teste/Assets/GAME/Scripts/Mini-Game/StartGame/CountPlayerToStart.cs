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

    public GameObject currentPlayer;
    public GameObject sceneMiniGame;
    public Transform SpawnSceneMinigame;
    public PhotonView otherPhotonView;
    public CharacterController controller;

    private bool minigameStarted = false;

    public float countdownTime = 5f;
    public TextMeshProUGUI countdownDisplay;

    public float currentTime;
    private bool timerIsActive = false;

    private new void OnEnable()
    {
        currentTime = countdownTime;
        minigameStarted = false;
    }

    void Update()
    {   
        if(otherPhotonView == null || !otherPhotonView.IsMine) { return; }

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

        if (currentTime <= 0 && !minigameStarted)
        {
            minigameStarted = true;

            timerIsActive = false;
            currentTime = 0;
            StartMinigame();
            this.gameObject.SetActive(false);
        }
    }

    void HideOtherPlayers()
    {
        Debug.Log("Escondendo outros jogadores");
        if (this.gameObject.CompareTag("Player") && otherPhotonView.IsMine)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                Debug.Log("Escondendo " + player);
                if (!player.GetComponent<PhotonView>().IsMine)
                {
                    player.SetActive(false);
                }
            }
        }
    }

    private void StartMinigame()
    {

        if (!onlineMinigame && otherPhotonView.IsMine)
        {
            Instantiate(sceneMiniGame, SpawnSceneMinigame.position, SpawnSceneMinigame.rotation);

            GameObject initialGameObject = GameObject.FindWithTag("InitialMiniGameTag");

            HideOtherPlayers();

            if (initialGameObject != null)
            {
                controller.enabled = false;

                currentPlayer.transform.position = initialGameObject.transform.position;
                currentPlayer.transform.rotation = initialGameObject.transform.rotation;

                controller.enabled = true;
                minigameHud.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        otherPhotonView = other.gameObject.GetComponent<PhotonView>();
        controller = other.gameObject.GetComponent<CharacterController>();
        currentPlayer = other.gameObject;

        if (currentPlayer.CompareTag("Player") && otherPhotonView != null && otherPhotonView.IsMine)
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
