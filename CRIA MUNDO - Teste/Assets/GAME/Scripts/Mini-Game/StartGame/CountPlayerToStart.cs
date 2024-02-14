using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountPlayerToStart : MonoBehaviourPunCallbacks
{

    public int playerToStart; //Quantidade de jogadores necessária para iniciar o minigame
    public int currentplayer; //Quantidade de jogadores no trigger

    public bool onlineMinigame = false; //Se o minigame é online ou local

    public string minigameName; //Nome do minigame
    public Sprite minigameIcon; //Icone do minigame

    [SerializeField] private Image icon; //HUD do icone do minigame
    public TextMeshProUGUI countPlayer; //HUD para a contagem de jogadores

    public GameObject currentPlayer; //Jogador atual
    public GameObject sceneMiniGame; //Prefab do minigame
    public Transform SpawnSceneMinigame; //Local de spawn do minigame
    public PhotonView otherPhotonView; //PhotonView do jogador atual
    public CharacterController controller; //Controller do jogador atual
    private bool timerIsActive = false;

    public GameObject hudCrefisa;

    private bool minigameStarted = false;

    public float countdownTime = 5f; // Tempo para iniciar o minigame
    public TextMeshProUGUI countdownDisplay; // HUD para a contagem do tempo

    public float currentTime;
   

    private new void OnEnable()
    {
        currentTime = countdownTime;
        minigameStarted = false;
    }

    void Update()
    {
        if (otherPhotonView == null || !otherPhotonView.IsMine) { return; }

        countPlayer.text = currentplayer.ToString();

        if (currentplayer >= playerToStart)
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

           
            currentTime = 0;
            StartMinigame();
            this.gameObject.SetActive(false);
        }
    }

    void HideOtherPlayers()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView pv = player.GetComponent<PhotonView>();
            if (pv != null && !pv.IsMine)
            {
                Debug.Log("Escondendo " + player.name);
                player.SetActive(false);
            }
            else
            {
                Debug.Log("Ignorando " + player.name + " porque é o jogador local ou não possui PhotonView");
            }
        }
    }

    public void StartMinigame()
    {

        if (!onlineMinigame && otherPhotonView.IsMine)
        {
            Instantiate(sceneMiniGame, SpawnSceneMinigame.position, SpawnSceneMinigame.rotation);

            GameObject initialGameObject = GameObject.FindWithTag("InitialMiniGameTag");

            PlayerVisibilityManager.HideOtherPlayers();

            if (initialGameObject != null)
            {
                controller.enabled = false;

                currentPlayer.transform.position = initialGameObject.transform.position;
                currentPlayer.transform.rotation = initialGameObject.transform.rotation;

                controller.enabled = true;
                hudCrefisa.SetActive(false);
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
            hudCrefisa.SetActive(true);
        }
            currentplayer++;

    }

    private void OnTriggerExit(Collider other)
    {
        PhotonView otherPhotonView = other.gameObject.GetComponent<PhotonView>();

        if (other.gameObject.CompareTag("Player") && otherPhotonView != null && otherPhotonView.IsMine)
        {
            hudCrefisa.SetActive(false);

        }
        currentplayer--;
        currentTime = countdownTime;
    }
}
