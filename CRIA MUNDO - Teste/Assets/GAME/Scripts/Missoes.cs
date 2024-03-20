using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Missoes : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider barra;
    public TextMeshProUGUI porcent;
    
    public float valorMax = 1000f;
    public GameObject Notificacao;
    public Button botao;
    public float currentValue;
    

    

    void Start()
    {
        
        currentValue = PlayerPrefs.GetFloat("CurrentValue", 0f);
        
        
       
        barra.maxValue = valorMax;
        barra.value = currentValue;

        float porcentagem = currentValue / valorMax * 100f;
        porcent.text = string.Format("{0}%", porcentagem);

       

    }

    public void BarraUpdate()
    {
        if (!botao.interactable)
            return;

        currentValue += 32f;

        float novaPorcentagem = currentValue / valorMax * 100f;

        barra.value = currentValue;
        porcent.text = string.Format("{0}%", novaPorcentagem);

        PlayerPrefs.SetFloat("CurrentValue", currentValue);

        PlayerPrefs.SetInt("LastClickTime", (int)DateTime.Now.Ticks);

        photonView.RPC("SomandoBarRPC", RpcTarget.AllBuffered, currentValue, novaPorcentagem);

        if (currentValue >= valorMax)
        {
            photonView.RPC("MensagemForAll", RpcTarget.All);
        }
    }

    [PunRPC]
    public void SomandoBarRPC(float novoValor, float novaPorcentagem)
    {
       
        barra.value = novoValor;
        porcent.text = string.Format("{0}%", novaPorcentagem);
    }

    [PunRPC]
    public void MensagemForAll()
    {
       
        if (!Notificacao.activeSelf)
        {
            Notificacao.SetActive(true);
           
        }
    }

    [PunRPC]
    public void MensagemOff()
    {
        Notificacao.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentValue);
        }
        else
        {
            currentValue = (float)stream.ReceiveNext();
            barra.value = currentValue;

            // Calcula a porcentagem e atualiza o texto
            float porcentagem = currentValue / valorMax * 100f;
            porcent.text = string.Format("{0}%", porcentagem);
        }
    }

   

    public void ResetarValoresSalvos()
    {
        
        currentValue = 0f;
       

        
        PlayerPrefs.SetFloat("CurrentValue", currentValue);
        photonView.RPC("MensagemOff", RpcTarget.All);
        
    }

   
}
