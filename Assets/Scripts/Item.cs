using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item: DragDrop
{
    public SpriteRenderer renderer;

    [SerializeField]
    private ColorAttr color;
    [SerializeField]
    private Shape shape;

    protected override void Awake()
    {
        if (renderer == null)
            renderer = GetComponentInChildren<SpriteRenderer>();
        if (color != null)
            setColor(color);
        if (shape != null)
            setShape(shape);
        base.Awake();
    }

    public void setColor(ColorAttr col)
    {
        color = col;
        renderer.color = color.rgb;
    }

    public void setShape(Shape shape)
    {
        this.shape = shape;
        renderer.sprite = shape.sprite;
        foreach (Collider2D col in renderer.GetComponents<Collider2D>())
        {
            Destroy(col);
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public bool hasCharacteristic(ICharacteristic ch)
    {
        return color.identifyAs(ch) || shape.identifyAs(ch);
    }
}
