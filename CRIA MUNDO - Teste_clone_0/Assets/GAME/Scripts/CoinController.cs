using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Variaveis Firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public int coinsValue;

    public DatabaseReference DBreference;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        LoadPlayerCoins();
    }

    private void LoadPlayerCoins()
    {
        string userId = FirebaseCORE.instance.authManager.user.UserId;
        DBreference.Child("users").Child(userId).Child("PlayerCoins").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists && int.TryParse(snapshot.Value.ToString(), out int coins))
                {
                    CORE.instance.status.playerCoin = coins;
                }
                else
                {
                    Debug.Log("PlayerCoins not found or invalid format.");
                }
            }
        });
    }


// Método para atualizar as moedas no Firebase
public void UpdatePlayerCoinsInFirebase(int coins)
        {
            if (FirebaseCORE.instance.authManager.user.UserId != null)
            {
                string userId = FirebaseCORE.instance.authManager.user.UserId;
                DatabaseReference playerCoinsRef = FirebaseDatabase.DefaultInstance.RootReference
                    .Child("users")
                    .Child(userId)
                    .Child("PlayerCoins");

                // Atualize esta linha para passar o valor inteiro das moedas
                playerCoinsRef.SetValueAsync(coins);
            }
            else
            {
                Debug.LogWarning("User not authenticated. Cannot update player coins.");
            }
        }
}
