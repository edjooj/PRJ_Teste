using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class TrocaPet : MonoBehaviourPunCallbacks
{
    public GameObject petCat;
    public GameObject petDog;

    public Image imaCat;
    public Image imaDog;

    public Image imaPet;
    public Image imaPetFront;
    public TMP_InputField petName;

    public Transform petSpawn;
    private GameObject currentPetInstance;
    private string currentPetName = string.Empty;
    [SerializeField]
    public int selectPetIndex = 0;
    
    public TMP_InputField nameCat;
    public TMP_InputField nameDog;

    // Start is called before the first frame update
    void Start()
    {
        petName.onEndEdit.AddListener(OnPetNameEndEdit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectCat()
    {
        selectPetIndex = 1;
        SpawnSelectPet();
    }

    public void SelectDog()
    {
        selectPetIndex = 2;
        SpawnSelectPet();

    }

    void SpawnSelectPet()
    {
        GameObject selectPetPrefab = null;
        switch (selectPetIndex)
        {
            case 1:
                selectPetPrefab = petCat;
                imaPet.sprite = imaCat.sprite;
                imaPetFront.gameObject.SetActive(true);
                imaPetFront.sprite = imaCat.sprite;
               
                break;

            case 2:
                selectPetPrefab = petDog;
                imaPet.sprite = imaDog.sprite;
                imaPetFront.gameObject.SetActive(true);
                imaPetFront.sprite = imaDog.sprite;
               
                break;

        }
        if (currentPetInstance != null)
        {
            Destroy(currentPetInstance);
        }

        if (selectPetPrefab != null)
        {
            currentPetInstance = Instantiate(selectPetPrefab, petSpawn.position, Quaternion.identity);
        }
    }

    private void OnPetNameEndEdit(string newName)
    {
        currentPetName = newName;
    }
}
