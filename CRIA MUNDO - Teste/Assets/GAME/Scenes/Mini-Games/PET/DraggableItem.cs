using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 initialPosition;
    public ParticleSystem Bubbles;
    public Camera petCAM;

    public LayerMask layerMask;

    public void OnBeginDrag(PointerEventData eventData) //Quando pegar o objeto
    {
        initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData) //Quando estiver com o objeto
    {
        Vector3 mouseWorldPosition = petCAM.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = initialPosition.z; 
        transform.position = mouseWorldPosition;

        mouseWorldPosition.z = petCAM.nearClipPlane;

        RaycastHit hit;

        if (Physics.Raycast(mouseWorldPosition, petCAM.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.tag == "PetBed")
            {
                Bubbles.Play();
            }
            else
            {
                Bubbles.Stop();
            }
        }
        
    }


    public void OnEndDrag(PointerEventData eventData) //Quando soltar o objeto
    {
        transform.position = initialPosition;
        Bubbles.Stop();
    }
}
