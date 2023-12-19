using Photon.Pun;
using UnityEngine;

public class DayNight : MonoBehaviourPunCallbacks
{
    public float dayLengthInSeconds = 120;
    public float timer = 0;
    public Light directionalLight;
    private float lastSyncTime = -1;

    public bool isNight;

    void Update()
    {
        timer += Time.deltaTime;
        float rotationAngle = (timer / dayLengthInSeconds) * 360;
        directionalLight.transform.rotation = Quaternion.Euler(rotationAngle, -30f, 0);

        if (timer >= dayLengthInSeconds) timer = 0;

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
