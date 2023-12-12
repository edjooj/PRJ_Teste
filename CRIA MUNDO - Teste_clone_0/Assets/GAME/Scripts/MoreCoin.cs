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

    // Function to be called when the button is pressed
    public void AddCoin()
    {
        CoinController.instance.coinsValue ++;
        CoinController.instance.UpdatePlayerCoinsInFirebase(CoinController.instance.coinsValue);
    }
}
