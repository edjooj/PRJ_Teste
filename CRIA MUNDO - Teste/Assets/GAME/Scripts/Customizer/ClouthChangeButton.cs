using Photon.Pun;

public class ClouthChangeButton : MonoBehaviourPun
{
    public Customize customizer;
    public Customize.CustomType customType;



    public void TrocarRoupa(int roupaIndice)
    {
        switch (customType)
        {
            case Customize.CustomType.CAMISA:
                NetworkController.instance.customize.camisa = roupaIndice;
                break;
            case Customize.CustomType.CABELO:
                NetworkController.instance.customize.cabelo = roupaIndice;
                break;
            case Customize.CustomType.CALÇA:
                NetworkController.instance.customize.calca = roupaIndice;
                break;
            case Customize.CustomType.CHAPEU:
                NetworkController.instance.customize.chapeu = roupaIndice;
                break;
            case Customize.CustomType.SAPATO:
                NetworkController.instance.customize.sapato = roupaIndice;
                break;
        }

        NetworkController.instance.customize.SaveCustomizePlayer();


    }
}
