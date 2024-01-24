using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);

        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.y += 180;
        transform.eulerAngles = eulerAngles;
    }
}