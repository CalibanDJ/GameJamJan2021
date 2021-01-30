using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Client : MonoBehaviour
{
    public DialogBubble dialogPrefab;
    public Transform dialogParent;
    public Camera cam;
    private DialogBubble lastBubble;

    public Shape desiredShape;
    public ColorAttr desiredColor;

    public IList<ICharacteristic> desiredCharacteristics;

    private void Awake()
    {
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

    public void reject()
    {
        if (lastBubble != null)
        {
            Destroy(lastBubble.gameObject);
        }
        Destroy(gameObject);
    }
}
