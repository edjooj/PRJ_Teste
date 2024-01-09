using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconData
{
    public List<IconInfo> ownedIcons;

    public PlayerIconData()
    {
        ownedIcons = new List<IconInfo>();
    }

    public void AddIcon(IconsDATA iconData)
    {
        var existingIcon = ownedIcons.Find(icon => icon.iconName == iconData.iconName);

        if (existingIcon == null)
        {
            ownedIcons.Add(new IconInfo(iconData.iconName, iconData.haveIcon));
            Debug.Log("Icon added: " + iconData.iconName);
        }
        else
        {
            Debug.Log("Icon already exists: " + iconData.iconName);
        }
    }


    public void SaveIconData(IconsDATA newIconData)
    {
        Debug.Log("Attempting to save icon data...");

        string userId = FirebaseCORE.instance.authManager.user.UserId;
        DatabaseReference playerIconRef = FirebaseDatabase.DefaultInstance.RootReference
            .Child("users")
            .Child(userId)
            .Child("IconPlayer")
            .Child("ownedIcons");

        playerIconRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error fetching icons from database: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Fetched icons data successfully.");

                DataSnapshot snapshot = task.Result;

                List<IconInfo> currentIcons = snapshot.Exists ? JsonUtility.FromJson<List<IconInfo>>(snapshot.GetRawJsonValue()) : new List<IconInfo>();

                var existingIcon = currentIcons.Find(icon => icon.iconName == newIconData.iconName);
                if (existingIcon == null)
                {
                    Debug.Log("Saving new icon: " + newIconData.iconName);

                    currentIcons.Add(new IconInfo(newIconData.iconName, true));
                    playerIconRef.SetRawJsonValueAsync(JsonUtility.ToJson(currentIcons));
                }
                else
                {
                    Debug.Log("Icon already owned, no need to save: " + newIconData.iconName);
                }
            }
        });
    }

    public void LoadIconData()
    {
        Debug.Log("Carregando dados do ícone...");

        var databaseRef = FirebaseDatabase.DefaultInstance.GetReference("players");

        databaseRef.Child("playerId").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Erro ao carregar dados do ícone: " + task.Exception.ToString());
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Dados do ícone carregados com sucesso.");
                DataSnapshot snapshot = task.Result;
                PlayerIconData playerData = JsonUtility.FromJson<PlayerIconData>(snapshot.GetRawJsonValue());
                UpdateUIWithIconData(playerData);
            }
        });
    }


    private void UpdateUIWithIconData(PlayerIconData playerData)
    {
        Debug.Log("Atualizando UI com dados dos ícones...");
        foreach (var iconId in playerData.ownedIcons)
        {
            Debug.Log("Ícone possuído: " + iconId);
        }
    }


}
