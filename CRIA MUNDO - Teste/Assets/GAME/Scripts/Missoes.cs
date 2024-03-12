using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Missoes : MonoBehaviourPunCallbacks
{
    public Slider barra;
    public TextMeshProUGUI porcent;
    public float valorMax = 1000f;
    PhotonView View;

    [Range(0f, 1000f)]
    private float currentValue = 0f;

    public Button button;

    [Range(0f, 100f)]
    private float porcentagem = 0f;

    private float action = 0f;

    public void Start()
    {
        barra.maxValue = valorMax;
        barra.value = currentValue;
    }

    public void BarraUpdate()
    {

        if (photonView.IsMine)
        {
            photonView.RPC("somandoBarRPC", RpcTarget.AllBuffered, currentValue += 32);
        }
        
       // currentValue += 32;
       // somandoBarRPC();
        Debug.Log(currentValue);

        porcentagem = currentValue / 10;

        if(currentValue >= valorMax)
        {
            porcentagem = 100f;
        }
       
        porcent.text = string.Format("{0}%", porcentagem);
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
    public void somandoBarRPC()
    {
        
        barra.value = currentValue;

        if(currentValue >= valorMax)
        {
            Debug.Log("Esta cheio");
        }

    }   


}
