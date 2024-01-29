using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject HudMiniGame;

    private int index;

    private void Start()
    {
        textComponent.text = string.Empty;
        StartDialogo();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    public void StartDialogo()
    {
        index = 0;
        textComponent.text = string.Empty;

        if (gameObject.activeSelf)
        {
            StartCoroutine(typeLine());
        }
    }

    IEnumerator typeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

   void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(typeLine());
        }
        else
        {
            
            gameObject.SetActive(false);
            HudMiniGame.SetActive(true);
        }
       
    }
    public void AtivarDialogoNovamente()
    {
        gameObject.SetActive(true);
        StartDialogo(); 
    }


}
