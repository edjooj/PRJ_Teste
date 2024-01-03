using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public static NetworkController instance;
    public PlayerCustomizer customize;
    public CoinController coinController;
    public ScoreController scoreController;

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
