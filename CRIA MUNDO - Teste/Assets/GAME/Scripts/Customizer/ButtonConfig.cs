using Photon.Pun;
using UnityEngine;
using static Customize;

public class ButtonConfig : MonoBehaviourPunCallbacks
{
    public int index;

    [SerializeField] private Customize customize;

    [Header("Scroolings")]
    public GameObject camisaScrooling, calcaScrooling, sapatoScrooling, cabeloScrooling, chapeuScrooling;

    [SerializeField] private ClouthColorChange clouthColorChange;


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

        clouthColorChange.UpdateCustomType(type);
        NetworkController.instance.customize.SaveRoupa();
    }
    #endregion

    public void ChooserMenu(string menu)
    {
        camisaScrooling.SetActive(false);
        calcaScrooling.SetActive(false);
        sapatoScrooling.SetActive(false);
        cabeloScrooling.SetActive(false);
        chapeuScrooling.SetActive(false);

        switch (menu)
        {
            case "camisa":
                camisaScrooling.SetActive(true);
                break;            
            
            case "calca":
                calcaScrooling.SetActive(true);
                break;            
            
            case "sapato":
                sapatoScrooling.SetActive(true);
                break;            
            
            case "cabelo":
                cabeloScrooling.SetActive(true);
                break;            
            
            case "chapeu":
                chapeuScrooling.SetActive(true);
                break;            
            
        }
    }




}
