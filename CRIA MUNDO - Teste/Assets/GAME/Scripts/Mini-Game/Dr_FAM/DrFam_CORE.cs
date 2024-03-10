using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFam_CORE : MonoBehaviour
{
    public static DrFam_CORE instance;
    public GameObject[] sitSpace;
    public GameObject[] camasDisponiveis;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
