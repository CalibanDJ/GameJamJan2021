using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item: DragDrop
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D phisicsBody;

    [SerializeField]
    private ColorAttr color;
    [SerializeField]
    private Shape shape;

    private ColorAttr oldColor;
    private Shape oldShape;

    private Client hoveredClient;

    protected override void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (phisicsBody == null)
            phisicsBody = GetComponentInChildren<Rigidbody2D>();
        if (color != null)
            setColor(color);
        if (shape != null)
            setShape(shape);
        base.Awake();
    }

    public void setColor(ColorAttr col)
    {
        if (col == oldColor) return;
        oldColor = col;

        color = col;
        spriteRenderer.color = color.rgb;
    }

    public void setShape(Shape shape)
    {
        if (shape == oldShape) return;
        oldShape = shape;

        this.shape = shape;
        spriteRenderer.sprite = shape.sprite;
        if (shape.material != null)
            phisicsBody.sharedMaterial = shape.material;
        if (Application.isPlaying)
        {
            foreach (Collider2D col in spriteRenderer.GetComponents<Collider2D>())
            {
                Destroy(col);
            }
            gameObject.AddComponent<PolygonCollider2D>();
        }
        else
        {
#if UNITY_EDITOR
            foreach (Collider2D col in spriteRenderer.GetComponents<Collider2D>())
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(col);
                };
            }
            UnityEditor.EditorApplication.delayCall += () =>
            {
                PolygonCollider2D coll = gameObject.AddComponent<PolygonCollider2D>();
            };
#endif
        }
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

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        if (hoveredClient != null)
        {
            hoveredClient.onGive(this);
        }
    }

    public void OnValidate()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (color != null)
            setColor(color);
        if (shape != null)
            setShape(shape);
    }
}
