using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;

    // Variaveis Firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public int coinsValue;

    public TMP_Text coin;

    private void Start()
    {
        coinsValue = 0;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Não foi possivel carregar as dependencias do Firebase " + dependencyStatus);
            }
        });
    }

    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }


    private void LoadPlayerCoinsFromFirebase()
    {
        if (user != null)
        {
            string userId = user.UserId;
            DatabaseReference playerCoinsRef = FirebaseDatabase.DefaultInstance.RootReference
                .Child("users")
                .Child(userId)
                .Child("PlayerCoins");

            playerCoinsRef.GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogWarning($"Failed to register task with {task.Exception}");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot != null && snapshot.Value != null)
                    {
                        string coins = snapshot.Value.ToString();

                        if (int.TryParse(coins, out coinsValue))
                        {
                            CORE.instance.status.playerCoin += coinsValue;
                            // Atualizar a interface ou executar outras ações necessárias
                        }
                    }
                }
            });
        }
        else
        {
            Debug.LogWarning("User not authenticated.");
        }
    }


    // Método para atualizar as moedas no Firebase
    public void UpdatePlayerCoinsInFirebase(int coins)
    {
        if (FirebaseAuthManager.instance.user != null)
        {
            string userId = FirebaseAuthManager.instance.user.UserId;
            DatabaseReference playerCoinsRef = FirebaseDatabase.DefaultInstance.RootReference
                .Child("users")
                .Child(userId)
                .Child("PlayerCoins");

            // Atualize esta linha para passar o valor inteiro das moedas
            playerCoinsRef.SetValueAsync(coins);
            coin.text = coinsValue.ToString();
        }
        else
        {
            Debug.LogWarning("User not authenticated. Cannot update player coins.");
        }
    }


}
