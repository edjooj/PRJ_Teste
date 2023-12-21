using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas : MonoBehaviour
{
    public ParticleSystem particulas;

    public void OnTriggerEnter(Collider other)
    {
            if(other.CompareTag("Player"))
        {
            particulas.Play();
            Destroy(this);
        }
    }
}
