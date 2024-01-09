using Firebase.Database;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIIconUse : MonoBehaviour
{
    [Header("IconData")]
    public IconsDATA icon;

    [Header("ContentConfig")]
    public Image iconImageContent;
    public Button iconButton;
    public float inactiveIconAlpha = 0.5f;
    public float activeIconAlpha = 1f;

    [Header("DescriptionConfig")]
    public Image iconImageDescription;
    public TMP_Text iconTxtDescription;
    public TMP_Text iconDescription;

    private DatabaseReference dbReference;

    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.GetReference("users");

        InitializeUIWithLocalData();
        LoadIconStateFromDatabase();
    }

    private void InitializeUIWithLocalData()
    {
        iconImageContent.sprite = icon.iconSprite;
        iconTxtDescription.text = icon.iconName;
        iconDescription.text = icon.iconDescription;

        SetIconInteractivity(icon.haveIcon);
    }

    private void LoadIconStateFromDatabase()
    {
        string userId = FirebaseCORE.instance.authManager.user.UserId;

        dbReference.Child(userId).Child("IconPlayer").Child("ownedIcons").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Erro ao carregar dados do ícone: " + task.Exception.ToString());
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot iconSnapshot in snapshot.Children)
                {
                    string iconName = iconSnapshot.Child("iconName").Value.ToString();
                    bool haveIcon = (bool)iconSnapshot.Child("haveIcon").Value;

                    if (iconName == icon.iconName)
                    {
                        StartCoroutine(SetIconInteractivityNextFrame(haveIcon));
                        break;
                    }
                }
            }
        });
    }

    private IEnumerator SetIconInteractivityNextFrame(bool haveIcon)
    {
        yield return null;

        SetIconInteractivity(haveIcon);
    }

    private void SetIconInteractivity(bool haveIcon)
    {
        iconButton.interactable = haveIcon;
        Color color = iconImageContent.color;
        color.a = haveIcon ? activeIconAlpha : inactiveIconAlpha;
        iconImageContent.color = color;
    }

    public void SelectedIcon()
    {
        iconImageDescription.sprite = icon.iconSprite;
        iconTxtDescription.text = icon.iconName;
        iconDescription.text = icon.iconDescription;
    }
}
