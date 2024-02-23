using EasyRoads3Dv3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGravacao : MonoBehaviour
{
    public Transform target; 
    public float speed = 10.0f;

    public bool moverCamera = false;

    private void Start()
    {
        StartCoroutine(CameraMoviment());
    }

    void Update()
    {
        if (moverCamera)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            transform.position = newPosition;
        }
    }

    IEnumerator CameraMoviment()
    {
        yield return new WaitForSeconds(3);

        moverCamera = true;

    }
}
