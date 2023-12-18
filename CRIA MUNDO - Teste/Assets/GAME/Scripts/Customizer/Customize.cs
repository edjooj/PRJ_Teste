using Photon.Pun;
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

    [System.Serializable]
    public class CustomizationVariations
    {
        public List<GameObject> camisas;
        public List<GameObject> calcas;
        public List<GameObject> sapatos;
        public List<GameObject> cabelos;
        public List<GameObject> chapeus;
    }

    private void Awake()
    {
        if(!photonView.IsMine)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void MeshSelect()
    {

        SelectVariation(CustomType.CAMISA, NetworkController.instance.customize.camisa);
        SelectVariation(CustomType.CALCA, NetworkController.instance.customize.calca);
        SelectVariation(CustomType.SAPATO, NetworkController.instance.customize.sapato);
        SelectVariation(CustomType.CABELO, NetworkController.instance.customize.cabelo);
        SelectVariation(CustomType.CHAPEU, NetworkController.instance.customize.chapeu);
    }

    public void SelectVariation(CustomType type, int index)
    {
        // Desativar todas as variações do tipo selecionado
        List<GameObject> currentTypeList = GetTypeList(type);
        if (currentTypeList != null)
        {
            foreach (var item in currentTypeList)
            {
                item.SetActive(false);
            }

            // Ativar a variação selecionada
            if (index >= 0 && index < currentTypeList.Count)
            {
                currentTypeList[index].SetActive(true);
            }
        }

        selectedObject = currentTypeList[index];
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


    #region CHAMADA HUD
    public void SelectCamisa(int index)
    {
        Debug.Log($"SelectCamisa: Selecionando camisa com índice {index}.");
        SelectVariation(CustomType.CAMISA, index);
        UpdateSkin(CustomType.CAMISA, index);
        NetworkController.instance.customize.camisa = index;
    }

    public void SelectCalca(int index)
    {
        SelectVariation(CustomType.CALCA, index);
        UpdateSkin(CustomType.CALCA, index);
        NetworkController.instance.customize.calca = index;
    }

    public void SelectSapato(int index)
    {
        SelectVariation(CustomType.SAPATO, index);
        UpdateSkin(CustomType.SAPATO, index);
        NetworkController.instance.customize.sapato = index;
    }

    public void SelectCabelo(int index)
    {
        SelectVariation(CustomType.CALCA, index);
        UpdateSkin(CustomType.CABELO, index);
        NetworkController.instance.customize.cabelo = index;
    }

    public void SelectChapeu(int index)
    {
        SelectVariation(CustomType.CHAPEU, index);
        UpdateSkin(CustomType.CHAPEU, index);
        NetworkController.instance.customize.chapeu = index;
    }
    #endregion
}
