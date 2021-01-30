using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Client : MonoBehaviour
{
    public DialogBubble dialogPrefab;
    public Transform dialogParent;
    public Camera cam;
    public Collider2D mouseCollider;
    private DialogBubble lastBubble;
    private WaitingLine line;

    public Shape desiredShape;
    public ColorAttr desiredColor;

    public IList<ICharacteristic> desiredCharacteristics;

    public void Start()
    {
        if (cam == null)
            cam = Camera.main;
        desiredCharacteristics = new List<ICharacteristic>();
        if (desiredShape != null)
            desiredCharacteristics.Add(desiredShape);
        if (desiredColor != null)
            desiredCharacteristics.Add(desiredColor);
    }

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

    public void onGive(Item item)
    {
        float score = desiredCharacteristics.Select((ch) => item.hasCharacteristic(ch)).Average(accepted => accepted ? 1.0f : 0.0f);

        Destroy(item.gameObject);
        reject();

        Debug.Log("Win score : " + score);

        // TODO add score
    }

    public void setSprite(Sprite sprite)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    public void setLine(WaitingLine line)
    {
        this.line = line;
        this.line.addClient(this);
    }

    public void setActive(bool active)
    {
        mouseCollider.enabled = active;
    }

    public void reject()
    {
        line.nextClient();
        if (lastBubble != null)
        {
            Destroy(lastBubble.gameObject);
        }
        Destroy(gameObject);
    }
}
