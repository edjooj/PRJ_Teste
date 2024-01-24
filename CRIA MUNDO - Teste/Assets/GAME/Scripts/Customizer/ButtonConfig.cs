using Photon.Pun;
using UnityEngine;
using static Customize;

public class ButtonConfig : MonoBehaviourPunCallbacks
{
    public int index;

    [SerializeField] private Customize customize;



    void Start()
    {
        if(!photonView.IsMine) { this.gameObject.SetActive(false); }

    }

    #region Definir o index das roupas nos botões

    public void OnCamisaButtonClicked(int index)
    {
        SelectCustomization(CustomType.CAMISA, index);
    }

    public void OnCalcaButtonClicked(int index)
    {
        SelectCustomization(CustomType.CALCA, index);
    }

    public void OnSapatoButtonClicked(int index)
    {
        SelectCustomization(CustomType.SAPATO, index);
    }

    public void OnCabeloButtonClicked(int index)
    {
        SelectCustomization(CustomType.CABELO, index);
    }

    public void OnChapeuButtonClicked(int index)
    {
        SelectCustomization(CustomType.CHAPEU, index);
    }

    private void SelectCustomization(CustomType type, int index)
    {
        if (!photonView.IsMine) { return; }

        switch (type)
        {
            case CustomType.CAMISA:
                customize.SelectCamisa(index);
                break;
            case CustomType.CALCA:
                customize.SelectCalca(index);
                break;
            case CustomType.SAPATO:
                customize.SelectSapato(index);
                break;
            case CustomType.CABELO:
                customize.SelectCabelo(index);
                break;
            case CustomType.CHAPEU:
                customize.SelectChapeu(index);
                break;

        }

      //  clouthColorChange.UpdateCustomType(type);
    }
    #endregion

    
    }





