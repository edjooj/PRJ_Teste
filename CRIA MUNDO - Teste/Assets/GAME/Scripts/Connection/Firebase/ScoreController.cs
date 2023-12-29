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
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference playerScoreRef = FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
                .Child(userId)
                .Child("PlayerScore");

            playerScoreRef.SetValueAsync(score);
        }
    }
}
