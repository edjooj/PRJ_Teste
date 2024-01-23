using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NPCProfessores : MonoBehaviourPunCallbacks
{
    public GameObject hudNPC;
    public Button botaoInteracao;
    public GameObject objetoParaAtivar;
    public Dialogo dialogo;

    public bool dialogoAtivo = false;

    private void Start()
    {
        hudNPC.SetActive(false);
        objetoParaAtivar.SetActive(false);
        botaoInteracao.onClick.AddListener(AtivarDialogo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && photonView.IsMine)
        {
            hudNPC.SetActive(true);
            if (!dialogoAtivo)
            {
                botaoInteracao.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && photonView.IsMine)
        {
            hudNPC.SetActive(false);
            DesativarDialogo();
        }
    }

    private void AtivarDialogo()
    {
        if (photonView.IsMine)
        {
            objetoParaAtivar.SetActive(true);
            dialogo.AtivarDialogoNovamente();
            dialogoAtivo = true;
            botaoInteracao.gameObject.SetActive(false);
        }
    }

    private void DesativarDialogo()
    {
        if (photonView.IsMine)
        {
            objetoParaAtivar.SetActive(false);
            dialogoAtivo = false;
            botaoInteracao.gameObject.SetActive(false);
        }
    }
}
