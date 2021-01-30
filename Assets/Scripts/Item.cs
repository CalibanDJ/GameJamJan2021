using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item: DragDrop
{
    private ColorAttr color;
    private Shape shape;

    public void setColor(ColorAttr col)
    {
        color = col;
    }

    public void setShape(Shape shape)
    {
        this.shape = shape;
    }

    public bool hasCharacteristic(ICharacteristic ch)
    {
        return color.identifyAs(ch) || shape.identifyAs(ch);
    }
}
