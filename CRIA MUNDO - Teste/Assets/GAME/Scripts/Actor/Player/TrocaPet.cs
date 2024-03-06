using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrocaPet : MonoBehaviour
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
        isCatSelected = true;
        isDogSelected = false;
        selectPetIndex = 1;
        SpawnSelectPet();
    }

    public void SelectDog()
    {
        isCatSelected = false;
        isDogSelected = true;
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

       
        petNameInputField.text = isCatSelected ? catName : dogName;

      
        if (currentPetInstance != null)
        {
            Destroy(currentPetInstance);
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
