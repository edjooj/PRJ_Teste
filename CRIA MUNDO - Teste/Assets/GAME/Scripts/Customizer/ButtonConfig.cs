using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConfig : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private int index;

    [SerializeField] private Customize.CustomType customType;

    [SerializeField] private Customize customize;

    void Start()
    {
        customize = FindObjectOfType<Customize>(); // Find the Customize script

        if (btn != null)
        {
            btn.onClick.AddListener(() => ApplyCustomization());
        }
    }

    private void ApplyCustomization()
    {
        switch (customType)
        {
            case Customize.CustomType.CAMISA:
                customize.SelectCamisa(index);
                break;
            case Customize.CustomType.CALCA:
                customize.SelectCalca(index);
                break;
            case Customize.CustomType.SAPATO:
                customize.SelectSapato(index);
                break;
            case Customize.CustomType.CABELO:
                customize.SelectCabelo(index);
                break;
            case Customize.CustomType.CHAPEU:
                customize.SelectChapeu(index);
                break;
            default:
                break;
        }
    }
}
