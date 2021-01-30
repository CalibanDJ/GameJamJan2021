using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item: DragDrop
{
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private ColorAttr color;
    [SerializeField]
    private Shape shape;

    private Client hoveredClient;

    protected override void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (color != null)
            setColor(color);
        if (shape != null)
            setShape(shape);
        base.Awake();
    }

    public void setColor(ColorAttr col)
    {
        color = col;
        spriteRenderer.color = color.rgb;
    }

    public void setShape(Shape shape)
    {
        this.shape = shape;
        spriteRenderer.sprite = shape.sprite;
        foreach (Collider2D col in spriteRenderer.GetComponents<Collider2D>())
        {
            Destroy(col);
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public bool hasCharacteristic(ICharacteristic ch)
    {
        return color.identifyAs(ch) || shape.identifyAs(ch);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Client cl = collision.GetComponent<Client>();
        if (cl != null)
        {
            hoveredClient = cl;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Client cl = collision.GetComponent<Client>();
        if (cl != null && cl == hoveredClient)
        {
            hoveredClient = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (hoveredClient != null)
        {
            hoveredClient.onGive(this);
        }
    }
}
