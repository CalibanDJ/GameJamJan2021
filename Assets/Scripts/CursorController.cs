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
        Cursor.SetCursor(clicked, new Vector2(clicked.width/2, clicked.height/2), CursorMode.ForceSoftware);
    }

    public void setUnclicked()
    {
        Cursor.SetCursor(unclicked, new Vector2(unclicked.width/2, unclicked.height/2), CursorMode.ForceSoftware);
    }
}
