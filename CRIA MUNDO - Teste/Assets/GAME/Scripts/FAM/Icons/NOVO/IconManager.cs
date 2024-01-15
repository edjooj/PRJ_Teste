using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class IconManager : MonoBehaviour
{
    public List<Icon> allIcons;
    public List<Icon> unlockedIcons = new List<Icon>();
    private string userId;

    public IconManagerFirebase iconManagerFirebase;

    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconPanel;

    public Image descriptionImage;
    public SpriteRenderer playerIcon;
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    private void Start()
    {
        userId = FirebaseCORE.instance.authManager.user.UserId;

        LoadIcons();
    }

    void OnEnable()
    {
        UpdateUI();
    }

    public void UnlockIcon(Icon icon)
    {
        if (!unlockedIcons.Contains(icon))
        {
            unlockedIcons.Add(icon);
            iconManagerFirebase.SaveUnlockedIcon(userId, icon.id);
        }
    }

    public void LoadIcons()
    {
        iconManagerFirebase.LoadUnlockedIcons(userId, (iconIds) =>
        {
            foreach (string iconId in iconIds)
            {
                Icon icon = allIcons.Find(i => i.id == iconId);
                if (icon != null)
                {
                    unlockedIcons.Add(icon);
                }
            }
            UpdateUI();
        }); 
    }

    public List<Icon> GetUnlockedIcons()
    {
        return unlockedIcons;
    }

    public void UpdateUI()
    {
        foreach (Transform child in iconPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Icon icon in unlockedIcons)
        {
            GameObject iconGO = Instantiate(iconPrefab, iconPanel);
            iconGO.GetComponent<Image>().sprite = icon.image;
            iconGO.GetComponent<Button>().interactable = true;

            iconGO.GetComponent<Button>().onClick.AddListener(() => UpdateDescriptionPanel(icon));
        }

    }
    public void UpdateDescriptionPanel(Icon icon)
    {
        descriptionImage.sprite = icon.image;
        playerIcon.sprite = icon.image;
        titleText.text = icon.name;
        descriptionText.text = icon.description;

    }

}
