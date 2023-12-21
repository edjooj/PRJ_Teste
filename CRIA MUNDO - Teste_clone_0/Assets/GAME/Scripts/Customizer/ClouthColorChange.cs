using Firebase.Auth;
using Firebase.Database;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomType = Customize.CustomType;


public class ClouthColorChange : MonoBehaviourPunCallbacks
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

    [PunRPC]
    void UpdateClothColorRPC(int playerActorNumber, string clothType, string colorString)
    {
        if (photonView.Owner.ActorNumber == playerActorNumber)
        {
            // É o jogador local que está fazendo a mudança
            custom.ApplyColor(clothType, colorString);
        }
        else
        {
            // É outro jogador, então encontre o objeto Customize correspondente e aplique a cor
            Customize otherPlayerCustomize = FindPlayerCustomizeObject(playerActorNumber);
            if (otherPlayerCustomize != null)
            {
                otherPlayerCustomize.ApplyColor(clothType, colorString);
            }
        }
    }


    private void ApplyColorToSpecificClothType(CustomType type, Color newColor)
    {
        foreach (var player in FindObjectsOfType<Customize>())
        {
            // Esta linha foi modificada para aplicar a cor independentemente do CustomType local.
            SkinnedMeshRenderer skinnedMeshRenderer = player.GetRendererForCustomType(type);
            if (skinnedMeshRenderer != null)
            {
                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                skinnedMeshRenderer.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_Color", newColor);
                skinnedMeshRenderer.SetPropertyBlock(propertyBlock);

                Debug.Log($"Aplicando cor {newColor} ao tipo {type} para o jogador {player.gameObject.name}.");
            }
            else
            {
                Debug.LogError($"Não foi possível encontrar SkinnedMeshRenderer para {type} no jogador {player.gameObject.name}.");
            }
        }
    }

    Customize FindPlayerCustomizeObject(int playerActorNumber)
    {
        foreach (var player in FindObjectsOfType<PhotonView>())
        {
            if (player.Owner.ActorNumber == playerActorNumber)
            {
                return player.GetComponent<Customize>();
            }
        }
        return null;
    }


    public void ApplyButton()
    {
        string colorString = ColorUtility.ToHtmlStringRGB(selectedColor);
        string clothType = custom.customType.ToString();
        int playerID = photonView.Owner.ActorNumber;

        SaveColorToFirebase(clothType, colorString);

        photonView.RPC("UpdateClothColorRPC", RpcTarget.AllBuffered, playerID, clothType, colorString);
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
                .Child("PlayerClothsColor")
                .Child(clothType);

            // Salva a cor como um valor direto, sem um nó separado para 'cor'
            clothRef.SetValueAsync(colorString);
        }
    }
}
