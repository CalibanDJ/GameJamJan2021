using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float aspectRatio;
    private Vector2Int res;
    private Camera cam;
    private float initialSize;
    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        initialSize = cam.orthographicSize;
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (res.x != Screen.width || res.y != Screen.height)
        {
            res.x = Screen.width;
            res.y = Screen.height;

            if (cam.aspect < aspectRatio)
            {
                cam.orthographicSize = initialSize * aspectRatio / cam.aspect;
                transform.position = new Vector3(transform.position.x, initialY + (cam.orthographicSize - initialSize), transform.position.z);
            }
            else
            {
                cam.orthographicSize = initialSize;
                transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
            }
        }
    }
}
