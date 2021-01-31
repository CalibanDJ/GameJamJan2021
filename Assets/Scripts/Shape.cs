using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape", menuName = "Shape!", order = 2)]
public class Shape : ScriptableObject, ICharacteristic
{
    public Sprite sprite;
    public PhysicsMaterial2D material;
    public string shapeName;

    public string getName()
    {
        return shapeName;
    }

    public virtual bool identifyAs(ICharacteristic ch)
    {
        return this == (UnityEngine.Object)ch;
    }
}
