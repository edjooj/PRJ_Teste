using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings dialogue;
    private List<string> sentences = new List<string>();

    bool playerHit;

    private void Start()
    {
        GetNpcInfo();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            DialogueControl.instance.Speech(sentences.ToArray());
        }
    }

    void GetNpcInfo()
    {
        for(int i = 0; i < dialogue.dialogues.Count; i++)
        {
            sentences.Add(dialogue.dialogues[i].sentence.ptBR);
        }
    }

    private void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);
        if(hits.Length == 0)
        {
            playerHit = false;
            DialogueControl.instance.dialogueObj.SetActive(false);
        }
        else
        {
            playerHit = true;
        }
        
        foreach (var hit in hits)
        {
            Debug.Log("Player is in range");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
