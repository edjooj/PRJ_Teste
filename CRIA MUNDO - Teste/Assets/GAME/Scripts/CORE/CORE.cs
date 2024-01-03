using UnityEngine;


public class CORE : MonoBehaviour
{
    public static CORE instance;
    public UserStatus status;
    public ConnectionPhoton connection;


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
