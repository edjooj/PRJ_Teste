using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NPCProfessores : MonoBehaviourPunCallbacks
{
    public GameObject hudNPC;
    public Button botaoInteracao;
    public GameObject objetoParaAtivar;
    public Dialogo dialogo;
    public Animator animator;

    public bool dialogoAtivo = false;

    private void Start()
    {
        hudNPC.SetActive(false);
        objetoParaAtivar.SetActive(false);
        botaoInteracao.onClick.AddListener(AtivarDialogo);

    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
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

        if (other.CompareTag("Player"))
            {
                hudNPC.SetActive(false);
                DesativarDialogo();
               
            }

        
    }

    private void AtivarDialogo()
    {
        

            objetoParaAtivar.SetActive(true);
             animator.SetBool("Boca", value: true);
           
        
            dialogo.AtivarDialogoNovamente();
            dialogoAtivo = true;
            botaoInteracao.gameObject.SetActive(false);
            

    }

    private void DesativarDialogo()
    {
       
        
            objetoParaAtivar.SetActive(false);
       
            dialogoAtivo = false;
            botaoInteracao.gameObject.SetActive(false);
            animator.SetBool("Boca", value: false);


    }
}
