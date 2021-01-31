using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeDrawer : MonoBehaviour
{
    public GameObject drawer;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(drawer, new Vector3(0, 0, 0), Quaternion.identity);
        go.GetComponent<Drawer>().SetSize(4, 2);
        go.transform.SetParent(canvas.transform);
        go.transform.SetSiblingIndex(0);
        go.transform.position = canvas.transform.position;
    }    
}
