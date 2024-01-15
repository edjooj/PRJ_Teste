using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

public class IconManagerFirebase : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveUnlockedIcon(string userId, string iconId)
    {
        databaseReference.Child("users").Child(userId).Child("unlockedIcons").Child(iconId).SetValueAsync(true);
    }

    public void LoadUnlockedIcons(string userId, System.Action<List<string>> onIconsLoaded)
    {
        databaseReference.Child("users").Child(userId).Child("unlockedIcons").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error loading icons");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<string> unlockedIcons = new List<string>();
                foreach (var icon in snapshot.Children)
                {
                    unlockedIcons.Add(icon.Key);
                }
                onIconsLoaded(unlockedIcons);
            }
        });
    }
}
