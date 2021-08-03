using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableTopBar : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler
{
    public GameObject parentWindow;

    private Vector2 prevMousePosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.prevMousePosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseDiff = eventData.position - this.prevMousePosition;

        RectTransform parentWindowRectTransform = this.parentWindow.GetComponent<RectTransform>();
        Vector2 modifiedPosition = parentWindowRectTransform.position;

        parentWindowRectTransform.position += mouseDiff;

        this.prevMousePosition = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
