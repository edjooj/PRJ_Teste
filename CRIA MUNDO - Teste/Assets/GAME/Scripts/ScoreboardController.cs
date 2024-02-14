using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    public static ScoreboardController instance;

    [Header("Database")]
    public FirebaseAuth auth;
    public string user;
    public DatabaseReference DBreference;

    [Header("Minigame - Crefisa")]
    public int crefisaScore; //Score do MiniGame da Crefisa

    [Header("Minigame - Linguas")]
    public string linguasTime; //Time do MiniGame de Linguas
    public int linguasClick; //Clicks do MiniGame de Linguas
    public int linguasLimpeza; //Limpeza do MiniGame de Linguas
    public int currentFaseLinguas; //Fase atual do curso de Linguas

    [Header("Minigame - Exatas")]
    public float exatasTime; //Score do MiniGame de Exatas
    public int exatasClick; //Click do MiniGame de Exatas

    [Header("Minigame - Biologicas")]
    public float biologicasScore; //Score do MiniGame de Biologicas

    [Header("Minigame - Design")]
    public float designScore; //Score do MiniGame de Design


    void Awake()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;
        user = FirebaseCORE.instance.authManager.user.UserId;

        if (instance == null)
        {
            instance = this;
        }

        GetLinguasPointsFromFirebase();
        GetExatasPointsFromFirebase();
    }

    public void UpdateCrefisaPointsInFirebase(int score)
    {
        if (!string.IsNullOrEmpty(FirebaseCORE.instance.authManager.user.UserId))
        {
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference playerScoreRef = FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
                .Child(userId)
                .Child("PlayerScore")
                .Child("Crefisa");

            playerScoreRef.SetValueAsync(score);
        }
    }

    #region Firebase Letras

    public void UpdateLinguasPointsInFirebase(string time, int click, int limpeza, int currentFaseLinguas)
    {
        if (!string.IsNullOrEmpty(FirebaseCORE.instance.authManager.user.UserId))
        {
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference linguasRef = FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
                .Child(userId)
                .Child("PlayerScore")
                .Child("Linguas");

            var linguasData = new Dictionary<string, object>
        {
            {"Timer", time},
            {"Click", click},
            {"Limpeza", limpeza},
            {"Current", currentFaseLinguas}
        };
            linguasRef.SetValueAsync(linguasData);
        }
    }

    public void GetLinguasPointsFromFirebase()
    {
        if (!string.IsNullOrEmpty(FirebaseCORE.instance.authManager.user.UserId))
        {
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference linguasRef = FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
                .Child(userId)
                .Child("PlayerScore")
                .Child("Linguas");

            linguasRef.GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Debug.Log("Deu erro");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChildren)
                    {
                        linguasTime = snapshot.Child("Timer").Value as string;
                        linguasClick = int.Parse(snapshot.Child("Click").Value.ToString());
                        linguasLimpeza = int.Parse(snapshot.Child("Limpeza").Value.ToString());
                        currentFaseLinguas = int.Parse(snapshot.Child("Current").Value.ToString());
                    }
                }
            });
        }
    }

    #endregion


    #region Firebase Exatas

    public void UpdateExatasPointsInFirebase(int time, int click)
    {
        if (!string.IsNullOrEmpty(FirebaseCORE.instance.authManager.user.UserId))
        {
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference exatasRef = FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
                .Child(userId)
                .Child("PlayerScore")
                .Child("Exatas");

            var exatasData = new Dictionary<string, object>
        {
            {"Timer", time},
            {"Click", click},
        };
            exatasRef.SetValueAsync(exatasData);
        }
    }

    public void GetExatasPointsFromFirebase()
    {
        if (!string.IsNullOrEmpty(FirebaseCORE.instance.authManager.user.UserId))
        {
            string userId = FirebaseCORE.instance.authManager.user.UserId;
            DatabaseReference exatasRef = FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
                .Child(userId)
                .Child("PlayerScore")
                .Child("Exatas");

            exatasRef.GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Debug.Log("Deu erro");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.HasChildren)
                    {
                        exatasTime = int.Parse(snapshot.Child("Timer").Value.ToString());
                        exatasClick = int.Parse(snapshot.Child("Click").Value.ToString());
                    }
                }
            });
        }
    }

    #endregion

}
