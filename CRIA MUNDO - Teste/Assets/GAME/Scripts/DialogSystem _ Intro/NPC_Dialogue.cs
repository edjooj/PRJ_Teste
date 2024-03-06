using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowDialogue()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, dialogueRange, playerLayer);
        foreach (var hit in hits)
        {
            Debug.Log("Player is in range");
        }
    }


}
