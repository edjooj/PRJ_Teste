using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseCORE : MonoBehaviour
{
    public static FirebaseCORE instance;
    public FirebaseAuthManager authManager;

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
