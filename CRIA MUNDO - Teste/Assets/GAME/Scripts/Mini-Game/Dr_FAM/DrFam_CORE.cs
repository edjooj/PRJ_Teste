using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFam_CORE : MonoBehaviour
{
    public bool startDrFamMiniGame = false;

    public static DrFam_CORE instance;
    public GameObject[] sitSpace;
    public bool[] chairOccupied;

    public GameObject[] camasDisponiveis;
    public bool[] camasOcupadas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        chairOccupied = new bool[sitSpace.Length];
        camasOcupadas = new bool[camasDisponiveis.Length];
    }
}
