using UnityEngine;

public class MoreCoin : MonoBehaviour
{
    public static MoreCoin instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        CORE.instance.status.playerCoin++;
        CoinController.instance.UpdatePlayerCoinsInFirebase(CORE.instance.status.playerCoin);
    }
}
