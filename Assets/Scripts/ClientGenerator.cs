using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGenerator : Generator
{
    public ItemGenerator itemGen;
    public Transform dialogParent;

    public Client clientPrefab;

    public WaitingLine[] wLines;
    public Sprite[] clientSprites;

    private void assignDesiredItem(Client c) {
        int shapeIdx = Random.Range(0, itemGen.getShapePoolSize());
        int colorIdx = Random.Range(0, itemGen.getColorPoolSize());

        c.desiredShape = itemGen.getGameData().shapes[shapeIdx];
        c.desiredColor = itemGen.getGameData().colors[colorIdx];
    }

    private void assignWaitingLine(Client c) {
        int WLineIdx = findSmallestLineIdx();

        if(WLineIdx == -1) {
            // All lines are full
            return;
        }
        c.setLine(wLines[WLineIdx]);
    }


    private int findSmallestLineIdx() {
        int idx = 0;
        int min = wLines[0].getClientCount();
        for(int i = 0; i < wLines.Length; ++i) {
            if(min > wLines[i].getClientCount()) {
                idx = i;
                min = wLines[i].getClientCount();
            }
        }
        return wLines[idx].isFull() ? -1 : idx;
    }

    public override void generate() {
        Client c = Instantiate(clientPrefab, new Vector2(0, 0), Quaternion.identity);
        c.dialogParent = dialogParent;
        c.setSprite(clientSprites[Random.Range(0, clientSprites.Length)]);
        assignDesiredItem(c);
        assignWaitingLine(c);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
