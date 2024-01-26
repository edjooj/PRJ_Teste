using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatusChange : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public int ticketsPlayer;
    public string playerNickname;
    public TMP_Dropdown courseDropdown;

    public PlayerInfo playerinfo;


    public DatabaseReference DBreference;

    void Start()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;

        StartCoroutine(LoadPlayerInfo());
    }

    private IEnumerator LoadPlayerInfo()
    {
        string userId = FirebaseCORE.instance.authManager.user.UserId;

        // Obter nickname do jogador
        var getPlayerNickTask = DBreference.Child("users").Child(userId).Child("PlayerNick").GetValueAsync();
        yield return new WaitUntil(() => getPlayerNickTask.IsCompleted);

        if (getPlayerNickTask.Exception != null || getPlayerNickTask.Result.Value == null)
        {
            Debug.LogError("Falha ao carregar o nickname do jogador.");
        }
        else
        {
            playerNickname = getPlayerNickTask.Result.Value.ToString();
        }

        // Obter tickets do jogador
        var getPlayerTicketsTask = DBreference.Child("users").Child(userId).Child("TicketsPlayer").GetValueAsync();
        yield return new WaitUntil(() => getPlayerTicketsTask.IsCompleted);

        if (getPlayerTicketsTask.Exception != null || getPlayerTicketsTask.Result.Value == null)
        {
            Debug.LogError("Falha ao carregar os tickets do jogador.");
            ticketsPlayer = 0;
        }
        else
        {
            ticketsPlayer = int.Parse(getPlayerTicketsTask.Result.Value.ToString());
        }
    }

    public void CourseChange()
    {
        playerinfo.playerCourse.text = courseDropdown.options[courseDropdown.value].text;
    }
}
