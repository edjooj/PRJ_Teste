using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Database")]
    public FirebaseAuth auth;
    public string user;
    public DatabaseReference DBreference;

    [Header("PlayerScore")]
    public int scorePoint;
    public int currentScorePoint;


    void Awake()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;
        user = FirebaseCORE.instance.authManager.user.UserId;

        StartCoroutine(LoadPlayerScore());
    }

    public void UpdatePlayerPointsInFirebase(int score)
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

    private IEnumerator LoadPlayerScore()
    {
        string userId = FirebaseCORE.instance.authManager.user.UserId;
        var getPlayerPointTask = DBreference.Child("users").Child(userId).Child("PlayerScore").GetValueAsync();

        yield return new WaitUntil(() => getPlayerPointTask.IsCompleted);

        if (getPlayerPointTask.IsCompleted)
        {
            DataSnapshot snapshot = getPlayerPointTask.Result;
            if (snapshot.Exists && int.TryParse(snapshot.Value.ToString(), out int points))
            {
                scorePoint = points;
            }
        }
    }
}
