using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClick : MonoBehaviour
{
    Vector2 mousePosition;

    public void ReceivePosition(Vector2 _mousePosition) 
    { 
        mousePosition = _mousePosition; 
    }

    public void OnMouseClick()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out hit ,Mathf.Infinity))
        {

            Debug.Log( hit.collider.name);
        }
        
    }
}
