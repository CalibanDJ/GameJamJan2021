using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGenerator : Generator
{
    public ItemGenerator itemGen;
    public Transform dialogParent;

    public Client clientPrefab;

    public WaitingLine[] wLines;

    private void assignDesiredItem(Client c) {
        int shapeIdx = Random.Range(0, itemGen.getShapePoolSize());
        int colorIdx = Random.Range(0, itemGen.getColorPoolSize());

        c.desiredShape = itemGen.getGameData().shapes[shapeIdx];
        c.desiredColor = itemGen.getGameData().colors[colorIdx];
    }

    private void assignWaitingLine(Client c) {
        int WLineIdx = Random.Range(0, wLines.Length);

        c.setLine(wLines[WLineIdx]);
    }

    public override void generate() {
        Client c = Instantiate(clientPrefab, new Vector2(0, 0), Quaternion.identity);
        c.dialogParent = dialogParent;
        assignDesiredItem(c);
        assignWaitingLine(c);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
