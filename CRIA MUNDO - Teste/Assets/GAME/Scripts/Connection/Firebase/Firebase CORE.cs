using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseCORE : MonoBehaviour
{
    public static FirebaseCORE instance;
    public FirebaseAuthManager authManager;
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
