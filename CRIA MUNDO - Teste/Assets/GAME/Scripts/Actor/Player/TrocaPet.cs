using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class TrocaPet : MonoBehaviourPunCallbacks
{

    public Image imaCat;
    public Image imaDog;

    public GameObject petCat;
    public GameObject petDog;

    public Image imaPet;
    public Image imaPetFront;
    public TMP_InputField petNameInputField;

    public int selectPetIndex = 0;

    public Transform petSpawn;
    private GameObject currentPetInstance;

    public string catName = "";
    public string dogName = "";

    private bool isCatSelected = false;
    private bool isDogSelected = false;

    private void Start()
    {
        petNameInputField.onEndEdit.AddListener(OnPetNameEndEdit);
        LoadPetNames();
    }

    public void SelectCat()
    { 
        SpawnSelectPet(); 
        selectPetIndex = 1;
        isCatSelected = true;
        isDogSelected = false; 
    }

    public void SelectDog()
    { 
        SpawnSelectPet();
        selectPetIndex = 2;
        isCatSelected = false;
        isDogSelected = true;
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


        petNameInputField.text = isCatSelected ? catName : dogName;

        if (currentPetInstance != null)
        {
            Destroy(currentPetInstance);
        }
        if (selectPetPrefab != null)
        {
            currentPetInstance = Instantiate(selectPetPrefab, petSpawn.position, petSpawn.rotation);
           // currentPetInstance.transform.SetParent(petSpawn);
        }
    }

    private void OnPetNameEndEdit(string newName)
    {
        if (isCatSelected)
        {
            catName = newName;
        }
        else if (isDogSelected)
        {
            dogName = newName;
        }

        SavePetName();
    }

    void SavePetName()
    {
        PlayerPrefs.SetString("CatName", catName);
        PlayerPrefs.SetString("DogName", dogName);
        PlayerPrefs.Save();
    }

    void LoadPetNames()
    {
        catName = PlayerPrefs.GetString("CatName", "");
        dogName = PlayerPrefs.GetString("DogName", "");
    }

}
