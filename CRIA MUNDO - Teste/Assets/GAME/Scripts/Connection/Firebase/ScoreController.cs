using Firebase.Auth;
using Firebase.Database;
using System.Drawing;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Database")]
    public FirebaseAuth auth;
    public string user;
    public DatabaseReference DBreference;

    [Header("PlayerScore")]
    public int scorePoint;


    void Awake()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;
        user = FirebaseCORE.instance.authManager.user.UserId;
    }

    public void UpdatePlayerPointsInFirebase(float score)
    {
        if (!string.IsNullOrEmpty(FirebaseCORE.instance.authManager.user.UserId))
        {
            DatabaseReference PlayerScoreRef = FirebaseDatabase.DefaultInstance.RootReference
                .Child("users")
                .Child(user)
                .Child("PlayerScore");

            PlayerScoreRef.SetValueAsync(score);
        }
    }
}
