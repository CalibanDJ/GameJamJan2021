using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public string layerName;
    public Camera cam;
    public TargetJoint2D mouseJoint;

    private int normalLayer;
    private int movingLayer;

    private void Awake()
    {
        if (mouseJoint == null)
            mouseJoint = GetComponentInChildren<TargetJoint2D>();
        mouseJoint.enabled = false;
        normalLayer = LayerMask.NameToLayer(layerName);
        movingLayer = LayerMask.NameToLayer(layerName + "Move");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseJoint.anchor = transform.InverseTransformPoint(eventData.pointerPressRaycast.worldPosition);
        mouseJoint.target = cam.ScreenToWorldPoint(eventData.position);
        mouseJoint.enabled = true;
        gameObject.layer = movingLayer;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mouseJoint.target = cam.ScreenToWorldPoint(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mouseJoint.enabled = false;
        gameObject.layer = normalLayer;
    }
}

