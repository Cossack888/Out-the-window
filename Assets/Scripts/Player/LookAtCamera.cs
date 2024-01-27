using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform cameraPosition;


    private void Awake()
    {
        cameraPosition = Camera.main.transform;
    }
    void FixedUpdate()
    {
        transform.LookAt(cameraPosition.position);
    }
}
