using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public ColorAttr[] colors;
    public Shape[] shapes;

    public static GameData Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }
}