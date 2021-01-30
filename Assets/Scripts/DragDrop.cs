using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public string layerName;
    public Camera cam;
    public TargetJoint2D mouseJoint;
    public Rigidbody2D itemBody;
    public float releaseMaxSpeed = 100.0f;

    private int normalLayer;
    private int movingLayer;

    protected virtual void Awake()
    {
        if (mouseJoint == null)
            mouseJoint = GetComponentInChildren<TargetJoint2D>();
        if (itemBody == null)
            itemBody = GetComponentInChildren<Rigidbody2D>();
        mouseJoint.enabled = false;
        normalLayer = LayerMask.NameToLayer(layerName);
        movingLayer = LayerMask.NameToLayer(layerName + "Move");
    }

    public void Start()
    {
        if (cam == null)
            cam = Camera.main;
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

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // Reset phisics
        mouseJoint.enabled = false;
        gameObject.layer = normalLayer;

        // Reduce angular speed
        itemBody.angularVelocity /= 4;

        // Limit max release speed
        float mag = itemBody.velocity.magnitude;
        if (mag > releaseMaxSpeed)
            itemBody.velocity = itemBody.velocity / mag * releaseMaxSpeed;
    }
}

