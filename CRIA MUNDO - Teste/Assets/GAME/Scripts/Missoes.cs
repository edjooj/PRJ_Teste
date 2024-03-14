using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Missoes : MonoBehaviourPunCallbacks, IPunObservable
{
    public Slider barra;
    public TextMeshProUGUI porcent;
    public float valorMax = 1000f;
    public GameObject Notficacao;
    

    [Range(0f, 1000f)]
    private float currentValue = 0f;

    public Button button;

    [Range(0f, 100f)]
    private float porcentagem = 0f;

    

    public void Start()
    {
        barra.maxValue = valorMax;
        barra.value = currentValue;
    }

    public void BarraUpdate()
    {

        // if (photonView.IsMine)

        currentValue += 32f;
            

           
        
        // currentValue += 32;
        // somandoBarRPC();
        Debug.Log(currentValue);
        

        porcentagem = currentValue / 10;
        if(currentValue >= valorMax)
        {
            porcentagem = 100f;
        }
        
        porcent.text = string.Format("{0}%", porcentagem);
        photonView.RPC("somandoBarRPC", RpcTarget.AllBuffered, currentValue, porcentagem);
        barra.value = currentValue;
        
    }

   /* private IEnumerator TweenTimer(float duration, Action<float> onTime = null, Action onEnd = null)
    {
        var _timer = 100f;
        while (_timer <= duration)
        {
            _timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            onTime?.Invoke(_timer);
        }
    }
   */

    [PunRPC]
    public void SomandoBarRPC(float newValue, float newPorcentagem)
    {
        currentValue = newValue;
        barra.value = newValue;
        porcentagem = newPorcentagem;
        

        if (currentValue >= valorMax)
        {
            Debug.Log("Esta cheio");
        }
        porcentagem = newValue / 10;
        if (newValue >= valorMax)
        {
            porcentagem = 100f;
        }
        porcent.text = string.Format("{0}%", porcentagem);
    }

    [PunRPC]
        public void MensagemForAll()
    {
        if (currentValue >= valorMax)
        {
            Notficacao.SetActive(true);
        }
    }


     
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(currentValue);
            stream.SendNext(porcentagem);
        }
        else
        {
            currentValue = (float)stream.ReceiveNext();
            barra.value = currentValue;
            porcentagem = (float)stream.ReceiveNext();
            porcent.text = string.Format("{0}%", porcentagem);

        }
    }

}
