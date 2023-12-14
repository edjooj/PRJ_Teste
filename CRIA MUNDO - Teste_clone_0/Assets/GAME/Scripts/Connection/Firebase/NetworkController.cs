using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static NetworkController instance;
    public PlayerCustomizer customize;
    public CoinController coinController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
