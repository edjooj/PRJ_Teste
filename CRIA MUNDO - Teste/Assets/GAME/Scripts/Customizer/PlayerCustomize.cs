using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomize : MonoBehaviour
{
    public int camisa;
    public int calça;
    public int luva;
    public int sapato;
    public int cabelo;
    public int chapeu;

    // Variaveis Firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public int coinsValue;

    public DatabaseReference DBreference;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        LoadCustomizePlayer();
    }

    public void LoadCustomizePlayer()
    {
        string userId = FirebaseCORE.instance.authManager.user.UserId;
    }

    public void SaveCustomizePlayer()
    {

    }
}
