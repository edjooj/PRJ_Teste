using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueControl : MonoBehaviour
{

    [Header("Components")]
    public GameObject dialogueObj; //Janela do diálogo
    public TextMeshProUGUI dialogueText; // Texto do diálogo

    [Header("Settings")]
    public float typingSpeed; // Velocidade emq ue vai aparecer as letras

    //Variaveis de Controle
    private bool isShowing; // Verifica se a janela de diálogo está aberta
    private int index; // Index do texto


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    IEnumerator Type()
    {
        foreach (char letter in dialogueText.text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
