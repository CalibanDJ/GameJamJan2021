using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public DialogBubble dialogPrefab;
    public Transform dialogParent;
    public Camera cam;
    private DialogBubble lastBubble;

    public void OnMouseDown()
    {
        if (lastBubble != null)
        {
            lastBubble.resetTimer();
        }
        else
        {
            DialogBubble bubble = Instantiate(dialogPrefab, cam.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0)), transform.rotation, dialogParent);
            bubble.setClient(this);
            lastBubble = bubble;
        }
    }

    public void reject()
    {
        if (lastBubble != null)
        {
            Destroy(lastBubble.gameObject);
        }
        Destroy(gameObject);
    }
}
