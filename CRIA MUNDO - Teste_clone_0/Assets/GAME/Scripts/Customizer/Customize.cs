using Photon.Pun;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviourPun
{
    [System.Serializable]
    public enum CustomType
    {
        CAMISA,CALCA, SAPATO, LUVA, CABELO, CHAPEU
    }

    public static Customize instance;

    public CustomizationVariations variations;

    public CustomType customType;

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
        instance = this;
    }

    public void MeshSelect()
    {
        Debug.Log("MeshSelect: Aplicando seleções de personalização.");

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

    public void EscolherCor(Color cor, CustomType tipo)
    {
        // Este método precisa ser atualizado para lidar com a escolha de cores das variações
        // Por exemplo, mudar a cor da camisa selecionada
        List<GameObject> currentTypeList = GetTypeList(tipo);
        if (currentTypeList != null)
        {
            foreach (var item in currentTypeList)
            {
                if (item.activeSelf)
                {
                    // Presumindo que o objeto tenha um Renderer com um Material que você deseja mudar
                    Renderer renderer = item.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = cor;
                    }
                    break; // Somente alterar o item ativo
                }
            }
        }
    }


    #region CHAMADA HUD
    public void SelectCamisa(int index)
    {
        Debug.Log($"SelectCamisa: Selecionando camisa com índice {index}.");
        SelectVariation(CustomType.CAMISA, index);
        NetworkController.instance.customize.camisa = index;
    }

    public void SelectCalca(int index)
    {
        SelectVariation(CustomType.CALCA, index);
        NetworkController.instance.customize.calca = index;
    }

    public void SelectSapato(int index)
    {
        SelectVariation(CustomType.SAPATO, index);
        NetworkController.instance.customize.sapato = index;
    }

    public void SelectCabelo(int index)
    {
        SelectVariation(CustomType.CALCA, index);
        NetworkController.instance.customize.cabelo = index;
    }

    public void SelectChapeu(int index)
    {
        SelectVariation(CustomType.CHAPEU, index);
        NetworkController.instance.customize.chapeu = index;
    }
    #endregion
}
