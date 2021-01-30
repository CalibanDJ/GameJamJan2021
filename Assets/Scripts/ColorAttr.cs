using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Color", menuName = "Color!", order = 1)]
public class ColorAttr: ScriptableObject, ICharacteristic
{
    public Color32 rgb;
    public string colorName;

    public bool identifyAs(ICharacteristic ch)
    {
        return this == (UnityEngine.Object)ch;
    }
}