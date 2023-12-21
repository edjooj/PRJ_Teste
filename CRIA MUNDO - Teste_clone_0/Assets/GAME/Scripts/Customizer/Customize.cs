using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Customize : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public enum CustomType
    {
        CAMISA,CALCA, SAPATO, LUVA, CABELO, CHAPEU
    }

    public CustomizationVariations variations;

    public CustomType customType;

    public GameObject selectedObject;

    public PlayerCustomizer playerCustomizer;

    [System.Serializable]
    public class CustomizationVariations
    {
        public List<GameObject> camisas;
        public List<GameObject> calcas;
        public List<GameObject> sapatos;
        public List<GameObject> cabelos;
        public List<GameObject> chapeus;
    }

    public void MeshSelect()
    {

        SelectVariation(CustomType.CAMISA, playerCustomizer.camisa);
        SelectVariation(CustomType.CALCA, playerCustomizer.calca);
        SelectVariation(CustomType.SAPATO, playerCustomizer.sapato);
        SelectVariation(CustomType.CABELO, playerCustomizer.cabelo);
        SelectVariation(CustomType.CHAPEU, playerCustomizer.chapeu);
    }

    public void SelectVariation(CustomType type, int index)
    {
        List<GameObject> currentTypeList = GetTypeList(type);
        if (currentTypeList != null && index >= 0 && index < currentTypeList.Count)
        {
            foreach (var item in currentTypeList)
            {
                item.SetActive(false);
            }
            currentTypeList[index].SetActive(true);
            selectedObject = currentTypeList[index];
        }
    }


    public SkinnedMeshRenderer GetSelectedObjectSkinnedMeshRenderer()
    {
        if (selectedObject != null)
        {
            return selectedObject.GetComponent<SkinnedMeshRenderer>();
        }

        return null;
    }


    private List<GameObject> GetTypeList(CustomType type)
    {
        switch (type)
        {
            case CustomType.CAMISA:
                return variations.camisas;
            case CustomType.CALCA:
                return variations.calcas;
            case CustomType.SAPATO:
                return variations.sapatos;
            case CustomType.CABELO:
                return variations.cabelos;
            case CustomType.CHAPEU:
                return variations.chapeus;
            default:
                return null;
        }
    }

    [PunRPC]
    void UpdateSkinRPC(CustomType type, int index)
    {
        SelectVariation(type, index);
    }

    public void UpdateSkin(CustomType type, int index)
    {
        photonView.RPC("UpdateSkinRPC", RpcTarget.AllBuffered, type, index);
    }

    public void ApplyColor(string clothType, string colorString)
    {
        CustomType type = (CustomType)Enum.Parse(typeof(CustomType), clothType);
        Color newColor;
        if (ColorUtility.TryParseHtmlString("#" + colorString, out newColor))
        {
            SkinnedMeshRenderer renderer = GetRendererForCustomType(type);
            if (renderer != null)
            {
                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor("_Color", newColor);
                renderer.SetPropertyBlock(propertyBlock);
            }
        }
    }


    public SkinnedMeshRenderer GetRendererForCustomType(CustomType type)
    {
        List<GameObject> typeList = GetTypeList(type);
        if (typeList != null)
        {
            // Encontre o objeto ativo atual para o tipo específico
            GameObject activeObject = typeList.Find(obj => obj.activeSelf);
            if (activeObject != null)
            {
                return activeObject.GetComponent<SkinnedMeshRenderer>();
            }
        }

        return null;
    }



    #region CHAMADA HUD
    public void SelectCamisa(int index)
    {
        Debug.Log($"SelectCamisa: Selecionando camisa com índice {index}.");
        SelectVariation(CustomType.CAMISA, index);
        UpdateSkin(CustomType.CAMISA, index);
        playerCustomizer.camisa = index;
    }

    public void SelectCalca(int index)
    {
        SelectVariation(CustomType.CALCA, index);
        UpdateSkin(CustomType.CALCA, index);
        playerCustomizer.calca = index;
    }

    public void SelectSapato(int index)
    {
        SelectVariation(CustomType.SAPATO, index);
        UpdateSkin(CustomType.SAPATO, index);
        playerCustomizer.sapato = index;
    }

    public void SelectCabelo(int index)
    {
        SelectVariation(CustomType.CALCA, index);
        UpdateSkin(CustomType.CABELO, index);
        playerCustomizer.cabelo = index;
    }

    public void SelectChapeu(int index)
    {
        SelectVariation(CustomType.CHAPEU, index);
        UpdateSkin(CustomType.CHAPEU, index);
        playerCustomizer.chapeu = index;
    }
    #endregion
}
