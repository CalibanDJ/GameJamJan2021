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

    public AudioSource audioSource;

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

        //audioSource.Play();
    }

    public void setSpawnPosition(Vector2 newPosition) {
        spawnPosition = newPosition;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        prepareInstance(1, 3, 0, 1, 20);
        base.Start();

        data = GameData.Instance;
        itemShapeGen = new GrowingRNG(data.shapes.Length, 1, 1, 20);
        itemColorGen = new GrowingRNG(data.colors.Length, 2, 1, 20);
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
