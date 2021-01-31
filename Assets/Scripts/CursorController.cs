using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public static CursorController Instance { get; private set; }

    public Texture2D clicked;
    public Texture2D unclicked;


    public void Awake()
    {
        Instance = this;
        setUnclicked();
    }

    // Update is called once per frame
    public void setClicked()
    {
        Cursor.SetCursor(clicked, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void setUnclicked()
    {
        Cursor.SetCursor(unclicked, Vector2.zero, CursorMode.ForceSoftware);
    }
}
