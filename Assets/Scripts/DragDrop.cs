using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Camera cam;
    public HingeJoint2D mouseJoint;
    public Collider2D objectCollider;

    private void Awake()
    {
        if (mouseJoint == null)
            mouseJoint = GetComponentInChildren<HingeJoint2D>();
        if (objectCollider == null)
            objectCollider = GetComponentInChildren<Collider2D>();
        mouseJoint.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        mouseJoint.enabled = true;
        mouseJoint.anchor = transform.InverseTransformPoint(eventData.pointerPressRaycast.worldPosition);
        mouseJoint.connectedAnchor = cam.ScreenToWorldPoint(eventData.position);
        objectCollider.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mouseJoint.connectedAnchor = cam.ScreenToWorldPoint(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        mouseJoint.enabled = false;
        objectCollider.enabled = true;
    }
}

