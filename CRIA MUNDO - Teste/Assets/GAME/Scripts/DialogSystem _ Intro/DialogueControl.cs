using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueControl : MonoBehaviour
{

    [Header("Components")]
    public GameObject dialogueObj; //Janela do diálogo
    public TextMeshProUGUI dialogueText; // Texto do diálogo
    public TMP_Text speechText; // Texto de quem está falando
    public TMP_Text actorName; // Texto de quem está falando

    [Header("Settings")]
    public float typingSpeed; // Velocidade emq ue vai aparecer as letras

    //Variaveis de Controle
    private bool isShowing; // Verifica se a janela de diálogo está aberta
    private int index; // Index do texto
    private string[] sentences; // Frases do diálogo

    public static DialogueControl instance;


    private void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() 
    { 
    
    }   

    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
