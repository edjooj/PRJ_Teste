using Photon.Pun;
using UnityEngine;

public class DayNight : MonoBehaviourPunCallbacks
{
    public float dayLengthInSeconds = 86400;
    [Range(0, 24)] public float timer = 0; 
    public Light directionalLight;
    private float lastSyncTime = -1;

    public bool isNight;

    void Update()
    {

        timer += Time.deltaTime / dayLengthInSeconds * 24; 


        float rotationAngle = (timer / 24) * 360; 
        directionalLight.transform.rotation = Quaternion.Euler(rotationAngle, -30f, 0);

        isNight = timer >= 18 || timer < 6; 


        if (timer >= 24) timer = 0;

        if (PhotonNetwork.IsMasterClient && Time.time - lastSyncTime > 5f)
        {
            lastSyncTime = Time.time;
            photonView.RPC("SyncTime", RpcTarget.Others, timer);
        }
    }

    [PunRPC]
    void SyncTime(float time)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            timer = time;
        }
    }
}
