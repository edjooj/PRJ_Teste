using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ClouthChangeButton : MonoBehaviourPun
{
    public Customize customizer;
    public Customize.CustomType customType;



    public void TrocarRoupa(int roupaIndice)
    {
        switch (customType)
        {
            case Customize.CustomType.CAMISA:
                CORE.instance.customize.camisa = roupaIndice;
                break;
            case Customize.CustomType.CABELO:
                CORE.instance.customize.cabelo = roupaIndice;
                break;
            case Customize.CustomType.CALÇA:
                CORE.instance.customize.calça = roupaIndice;
                break;
            case Customize.CustomType.CHAPEU:
                CORE.instance.customize.chapeu = roupaIndice;
                break;
            case Customize.CustomType.SAPATO:
                CORE.instance.customize.sapato = roupaIndice;
                break;
        }

        CORE.instance.customize.SaveCustomizePlayer();


    }
}
