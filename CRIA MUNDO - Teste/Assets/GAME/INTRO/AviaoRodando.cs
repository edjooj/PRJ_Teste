using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviaoRodando : MonoBehaviour
{
    public Transform island;
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        RodarAviao();
    }

    public void RodarAviao()
    {
        //Rodar em torno da ilha
        transform.RotateAround(island.position, Vector3.up, Speed * Time.deltaTime);
    }

}
