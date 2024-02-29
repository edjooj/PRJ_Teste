using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 initialPosition;
    public ParticleSystem Bubbles;

    public void OnBeginDrag(PointerEventData eventData) //Quando pegar o objeto
    {
        Debug.Log("Pegou o objeto");
        initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData) //Quando estiver com o objeto
    {
        Debug.Log("Está com o objeto");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) //Quando soltar o objeto
    {
        Debug.Log("Soltou o objeto");
        transform.position = initialPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pet")
        {
            Bubbles.Play();
        }
    }
}
