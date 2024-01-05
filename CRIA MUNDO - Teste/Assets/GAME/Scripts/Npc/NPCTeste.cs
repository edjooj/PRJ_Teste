using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;
using UnityEngine.UI;


public class NPCInteraction : MonoBehaviourPunCallbacks
{
    public GameObject hudNPC;
    public Button botaoInteracao;
    public GameObject objetoParaAtivar;
    public Dialogo dialogo;
    public Transform cabecaNPC;

    public bool dialogoAtivo = false;
    private NavMeshAgent agent;

    private void Start()
    {
        hudNPC.SetActive(false);
        objetoParaAtivar.SetActive(false);
        botaoInteracao.onClick.AddListener(AtivarDialogo);
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (hudNPC.activeSelf && cabecaNPC != null)
        {
           // OrientarCabecaParaJogador();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PararNPC();
           // if (!photonView.IsMine) { return; }
            hudNPC.SetActive(true);
            if (!dialogoAtivo)
            {
                botaoInteracao.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudNPC.SetActive(false);
            DesativarDialogo();
            ContinuarNPC();
        }
    }

    private void AtivarDialogo()
    {
        objetoParaAtivar.SetActive(true);
        dialogo.AtivarDialogoNovamente(); 
        dialogoAtivo = true;
        botaoInteracao.gameObject.SetActive(false);
    }


    private void DesativarDialogo()
    {
        objetoParaAtivar.SetActive(false);
        dialogoAtivo = false;
        botaoInteracao.gameObject.SetActive(false);
    }

   // private void OrientarCabecaParaJogador()
    //{
      //  if (cabecaNPC != null)
        //{
          //  Vector3 direcaoDoJogador = (Camera.main.transform.position - cabecaNPC.position).normalized;
            //cabecaNPC.LookAt(cabecaNPC.position + new Vector3(direcaoDoJogador.x, 0f, direcaoDoJogador.z));
        //}
    //}

    private void PararNPC()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        
    }

    private void ContinuarNPC()
    {
        agent.isStopped = false;
        agent.speed = 1.18f;
    }
}
