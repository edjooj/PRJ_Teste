using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomType = Customize.CustomType;


public class ClouthColorChange : MonoBehaviour
{
    [SerializeField] Slider rgbSlider;
    [SerializeField] Color selectedColor;

    DatabaseReference dbReference;
    public FirebaseAuth auth;
    public FirebaseUser user;

    [SerializeField] Customize custom;

    void Start()
    {
        auth = FirebaseCORE.instance.authManager.auth;
        dbReference = FirebaseCORE.instance.authManager.DBreference;
    }

    public void UpdateCustomType(CustomType type)
    {
        custom.customType = type;
    }

    public void RGBSlider()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = custom.GetSelectedObjectSkinnedMeshRenderer();
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

        if (skinnedMeshRenderer != null)
        {
            var hue = rgbSlider.value;
            selectedColor = Color.HSVToRGB(hue, 1f, 1f);

            skinnedMeshRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_Color", selectedColor);
            skinnedMeshRenderer.SetPropertyBlock(propertyBlock);
        }
    }



    private bool ShouldChangeColor(string materialName, CustomType customType)
    {

        switch (customType)
        {
            case CustomType.CAMISA:
                return materialName.Contains("Camisa");
            case CustomType.CABELO:
                return materialName.Contains("Cabelo");
            case CustomType.CALCA:
                return materialName.Contains("Calca");
            case CustomType.CHAPEU:
                return materialName.Contains("Chapeu");
            case CustomType.SAPATO:
                return materialName.Contains("Sapato");
            default:
                return false;
        }
    }




    public void ApplyButton()
    {
        // Converte a cor selecionada para uma string hexadecimal RGB
        string colorString = ColorUtility.ToHtmlStringRGB(selectedColor);

        // Obtém o tipo de roupa atualmente selecionado no script Customize
        string clothType = custom.customType.ToString();

        SaveColorToFirebase(clothType, colorString);
    }


    private void SaveColorToFirebase(string clothType, string colorString)
    {
        if (auth.CurrentUser != null)
        {
            string userId = auth.CurrentUser.UserId;
            // Caminho para o tipo específico de roupa do usuário no Firebase
            DatabaseReference clothRef = dbReference
                .Child("users")
                .Child(userId)
                .Child("PlayerCloths")
                .Child(clothType);

            // Salva a cor como um valor direto, sem um nó separado para 'cor'
            clothRef.SetValueAsync(colorString).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError($"Erro ao salvar a cor para {clothType} no Firebase: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    Debug.Log($"Cor para {clothType} salva com sucesso no Firebase.");
                }
            });
        }
        else
        {
            Debug.LogWarning("User not authenticated. Cannot save cloth color.");
        }
    }
}
