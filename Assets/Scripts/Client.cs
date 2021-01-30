using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Client : MonoBehaviour
{
    public float leaveTime = 10.0f;
    public DialogBubble dialogPrefab;
    public Transform dialogParent;
    public Camera cam;
    public Collider2D mouseCollider;
    private DialogBubble lastBubble;
    private WaitingLine line;

    public Shape desiredShape;
    public ColorAttr desiredColor;

    public IList<ICharacteristic> desiredCharacteristics;
    public float leaveTimer;

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
        bool allCorrect = desiredCharacteristics.All(ch => item.hasCharacteristic(ch));
        int score = allCorrect ? desiredCharacteristics.Count : 0;

        Destroy(item.gameObject);
        dispawn();

        GameScore.Instance.addScore(score);
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
        if (active)
        {
            leaveTimer = leaveTime;
        }
    }

    public void Update()
    {
        leaveTimer -= Time.deltaTime;
        if (leaveTimer <= 0.0f)
        {
            GameScore.Instance.addScore(0);
            dispawn();
        }
    }

    public void reject()
    {
        Transform itemParent = ItemGenerator.Instance.ItemsGameObject.transform;
        bool itemPresent = itemParent.GetComponentsInChildren<Item>().Any(item => desiredCharacteristics.All(ch => item.hasCharacteristic(ch)));

        if (itemPresent)
        {
            GameScore.Instance.addScore(0);
        }
        
        dispawn();
    }

    private void dispawn()
    {
        line.nextClient();
        if (lastBubble != null)
        {
            Destroy(lastBubble.gameObject);
        }
        Destroy(gameObject);
    }
}
