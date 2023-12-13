using UnityEngine;


public class CORE : MonoBehaviour
{
    public static CORE instance;
    public UserStatus status;
    public ConnectionPhoton connection;
    public PlayerCustomize customize;

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
