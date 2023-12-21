using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject LoginPanel;
    [SerializeField] private GameObject RegistrationPanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void OpenLoginPanel()
    {
        LoginPanel.SetActive(true);
        RegistrationPanel.SetActive(false);
    }

    public void OpenRegistrationPanel()
    {
        LoginPanel.SetActive(false);
        RegistrationPanel.SetActive(true);
    }
}
