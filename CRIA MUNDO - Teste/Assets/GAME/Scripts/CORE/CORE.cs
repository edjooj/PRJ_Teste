using UnityEngine;


public class CORE : MonoBehaviour
{
    public static CORE instance;
    public UserStatus status;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<CORE>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
