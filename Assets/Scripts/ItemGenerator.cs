using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemGen;

public class ItemGenerator : Generator
{

    private GrowingRNG itemShapeGen;
    private GrowingRNG itemColorGen;

    private Vector2 spawnPosition;
    private GameData data;

    public Item itemPrefab;
    public GameObject ItemsGameObject;

    public static ItemGenerator Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public override void generate() {
        int shapeIdx = itemShapeGen.generateNumber();
        int colorIdx = itemColorGen.generateNumber();

        Item i = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        i.transform.SetParent(ItemsGameObject.transform);
        i.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        i.transform.GetComponent<SpriteRenderer>().sortingOrder = 5;
        i.setColor(data.colors[colorIdx]);
        i.setShape(data.shapes[shapeIdx]);
    }

    public void setSpawnPosition(Vector2 newPosition) {
        spawnPosition = newPosition;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        prepareInstance(0, 2, 0, 1);
        base.Start();

        data = GameData.Instance;
        itemShapeGen = new GrowingRNG(data.shapes.Length, data.shapes.Length / 2, 1);
        itemColorGen = new GrowingRNG(data.colors.Length, data.colors.Length / 2, 1);
        setSpawnPosition(transform.position);
    }

    public int getShapePoolSize() {
        return itemShapeGen.getCurrentPoolSize();
    }

    public int getColorPoolSize() {
        return itemColorGen.getCurrentPoolSize();
    }

    public GameData getGameData() {
        return data;
    }
}
