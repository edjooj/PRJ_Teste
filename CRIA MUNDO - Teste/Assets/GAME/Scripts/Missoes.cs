using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Missoes : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider barra;
    public TextMeshProUGUI porcent;
    public TextMeshProUGUI time;
    public float valorMax = 1000f;
    public GameObject Notificacao;
    public Button botao;
    public float currentValue;
    public float timeValue = 90f;

    public DateTime lastClickTime;

    void Start()
    {
        
        currentValue = PlayerPrefs.GetFloat("CurrentValue", 0f);
        long lastClickTicks = PlayerPrefs.GetInt("LastCLickTime", 0);
        lastClickTime = new DateTime(lastClickTicks);
       
        barra.maxValue = valorMax;
        barra.value = currentValue;

        float porcentagem = currentValue / valorMax * 100f;
        porcent.text = string.Format("{0}%", porcentagem);

        BotaoDesativar();

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

    public void BotaoDesativar()
    {
        if((DateTime.Now - lastClickTime).TotalDays >= 1)
        {
            botao.interactable = true;
        }
        else 
        {
             botao.interactable = false;
        }
    
    }

    public void ResetarValoresSalvos()
    {
        
        currentValue = 0f;
        lastClickTime = DateTime.Now;

        
        PlayerPrefs.SetFloat("CurrentValue", currentValue);
        PlayerPrefs.SetInt("LastClickTime", (int)lastClickTime.Ticks);
    }

   
}
