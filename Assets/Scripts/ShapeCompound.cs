using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape", menuName = "ShapeCompound!", order = 3)]
public class ShapeCompound : Shape
{
    public Shape simplifiedShape;

    public override bool identifyAs(ICharacteristic ch)
    {
        return base.identifyAs(ch) || simplifiedShape.identifyAs(ch);
    }
}
