using Firebase.Auth;
using Firebase;
using UnityEngine;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PET_ManagerFirebase : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public string user;
    public DatabaseReference DBreference;

    [Header("PET")]
    public DataPET[] dataPET;
    public int petIndex;
    public int[] petAvailable;


    [Header("PET Info")]
    public string petName;
    public int petLevel;
    public float petExperience;
    public float petHunger;
    public float petHappiness;
    public float petCleanliness;
    public TMP_Text petNameText;

    private void Awake()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        DBreference = FirebaseCORE.instance.authManager.DBreference;
        user = FirebaseCORE.instance.authManager.user.UserId;
    }

    public void LoadPET()
    {
        StartCoroutine(CheckAuthenticationAndLoadPET());
    }

    private IEnumerator CheckAuthenticationAndLoadPET()
    {
        yield return new WaitUntil(() => auth.CurrentUser != null); // Espera pela autenticação do usuário.

        if (auth.CurrentUser != null)
        {
            Debug.Log("Usuário está autenticado com UID: " + auth.CurrentUser.UserId);
            StartCoroutine(LoadPlayerPET());
        }
        else
        {
            Debug.LogWarning("Usuário não está autenticado!");
        }
    }

    private IEnumerator LoadPlayerPET()
    {
        var getPlayerPETTask = DBreference.Child("users").Child(user).Child("PlayerPET").GetValueAsync();

        yield return new WaitUntil(() => getPlayerPETTask.IsCompleted);

        if (getPlayerPETTask.IsFaulted)
        {
            // Handle the error...
            Debug.LogError(getPlayerPETTask.Exception);
        }
        else if (getPlayerPETTask.IsCompleted)
        {
            DataSnapshot snapshot = getPlayerPETTask.Result;
            if (snapshot.Exists)
            {
                petIndex = int.Parse(snapshot.Child("petIndex").Value.ToString());
                petName = snapshot.Child("petName").Value.ToString();

                Debug.Log("PET carregado com sucesso!");
            }
            else
            {
                Debug.LogWarning("PET não encontrado!");
            }
        }
    }

    #region Save PET
    public void SavePET()
    {
        StartCoroutine(CheckAuthenticationAndSavePET());
    }

    private IEnumerator CheckAuthenticationAndSavePET()
    {
        yield return new WaitUntil(() => auth.CurrentUser != null); // Espera pela autenticação do usuário.

        if (auth.CurrentUser != null)
        {
            Debug.Log("Usuário está autenticado com UID: " + auth.CurrentUser.UserId);
            StartCoroutine(SavePlayerPET());
        }
        else
        {
            Debug.LogWarning("Usuário não está autenticado!");
        }
    }

    private IEnumerator SavePlayerPET()
    {
        
            Dictionary<string, object> petData = new Dictionary<string, object>
        {
            {"petIndex", petIndex},
            {"petName", petNameText.text},
        };

        var savePlayerPETTask = DBreference.Child("users").Child(user).Child("PlayerPET").SetValueAsync(petData);

        yield return new WaitUntil(() => savePlayerPETTask.IsCompleted);

        if (savePlayerPETTask.IsFaulted)
        {
            // Tratar o erro...
            Debug.LogError(savePlayerPETTask.Exception);
        }
        else if (savePlayerPETTask.IsCompleted)
        {
            Debug.Log("PET salvo com sucesso!");
        }
    }

    #endregion
}
