using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Missoes : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider barra;
    public TextMeshProUGUI porcent;
    public float valorMax = 1000f;
    public GameObject Notificacao;

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

         
            float novaPorcentagem = currentValue / valorMax * 100f;

            
            barra.value = currentValue;
            porcent.text = string.Format("{0}%", novaPorcentagem);

            photonView.RPC("SomandoBarRPC", RpcTarget.AllBuffered, currentValue, novaPorcentagem);

            
            if (currentValue >= valorMax)
            {
                photonView.RPC("MensagemForAll", RpcTarget.All);
            }
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
            porcent.text = string.Format("{0}%", currentValue / valorMax * 100f);
        }
    }
}
