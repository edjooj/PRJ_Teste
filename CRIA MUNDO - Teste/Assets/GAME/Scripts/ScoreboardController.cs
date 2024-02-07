using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLayerInfoScore
{
    public string username;
    public int score;
    public string test;
}

public class ScoreboardController : MonoBehaviour
{
    [Header("Database")]
    public FirebaseAuth auth;
    public string user;
    public DatabaseReference DBreference;

    public TMP_Text NicknamePrimeiro, NicknameSegundo, NicknameTerceiro;
    public TMP_Text ScorePrimeiro, ScoreSegundo, ScoreTerceiro;

    void Awake()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;
        user = FirebaseCORE.instance.authManager.user.UserId;

        LoadScoreboard();
    }

    public void LoadScoreboard()
    {
        StartCoroutine(LoadScoreboardCoroutine());
    }

    private IEnumerator LoadScoreboardCoroutine()
    {
        var getScoreboardTask = DBreference.Child("users").OrderByChild("PlayerScore").LimitToLast(3).GetValueAsync();

        yield return new WaitUntil(() => getScoreboardTask.IsCompleted);

        if (getScoreboardTask.IsCompleted)
        {
            DataSnapshot snapshot = getScoreboardTask.Result;
            if (snapshot.Exists)
            {
                List<PLayerInfoScore> players = new List<PLayerInfoScore>();
                foreach (DataSnapshot player in snapshot.Children)
                {
                    PLayerInfoScore playerInfo = new PLayerInfoScore();
                    playerInfo.username = player.Child("username").Value.ToString();
                    playerInfo.score = int.Parse(player.Child("PlayerScore").Value.ToString());
                    players.Add(playerInfo);
                }

                players.Sort((x, y) => y.score.CompareTo(x.score));

                NicknamePrimeiro.text = players[0].username;
                ScorePrimeiro.text = players[0].score.ToString();

                NicknameSegundo.text = players[1].username;
                ScoreSegundo.text = players[1].score.ToString();

                NicknameTerceiro.text = players[2].username;
                ScoreTerceiro.text = players[2].score.ToString();
            }
        }
    }
}
