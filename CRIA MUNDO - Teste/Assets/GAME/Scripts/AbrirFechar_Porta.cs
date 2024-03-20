using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirFechar_Porta : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isOpen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isOpen", false);
    }
}
