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
    public float porcentagem = 0f;

    private float currentValue = 0f;

    public void Start()
    {
        barra.maxValue = valorMax;
        barra.value = currentValue;
    }

    public void BarraUpdate()
    {
        {

            currentValue += 32f;
            porcentagem = currentValue / valorMax * 100f;
            
            barra.value = currentValue;
            porcent.text = string.Format("{0}%", porcentagem);

            photonView.RPC("SomandoBarRPC()", RpcTarget.AllBuffered, currentValue, porcentagem);
            
            if (currentValue >= valorMax)
            {
                photonView.RPC("MensagemForAll()", RpcTarget.All);
                porcent.text = string.Format("100%");
            }

        }
    }

    [PunRPC]
    public void SomandoBarRPC(float novoValor, float novaPorcentagem)
    {
        currentValue = novoValor;
        barra.value = novoValor;
        porcentagem = novaPorcentagem;
        porcent.text = string.Format("{0}%", novaPorcentagem);
        if (currentValue >= valorMax)
        {
            
            porcent.text = string.Format("100%");
        }

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
            stream.SendNext(porcentagem);
        }
        else
        {
            currentValue = (float)stream.ReceiveNext();
            barra.value = currentValue;
            porcent.text = string.Format("{0}%", currentValue / valorMax * 100f);
        }

    }
}
