using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemGen;

public class ItemGenerator : Generator
{

    private GrowingRNG itemShapeGen;
    private GrowingRNG itemColorGen;
    
    private GameData data;

    public Item itemPrefab;
    public GameObject ItemsGameObject;

    public static ItemGenerator Instance { get; private set; }

    public AudioSource audioSource;
    private float itemZOffset = 0.0f;

    public void Awake()
    {
        Instance = this;
    }

    public override void generate() {
        int shapeIdx = itemShapeGen.generateNumber();
        int colorIdx = itemColorGen.generateNumber();

        float randX = Random.Range(0.0f, 2.0f);
        int randY = Random.Range(0, 3);
        int randAngles = Random.Range(0, 360);

        Vector3 pos = new Vector3(transform.position.x + randX, transform.position.y + randY, 1 + nextItemZOffset());
        Item i = Instantiate(itemPrefab, pos, Quaternion.AngleAxis(randAngles, Vector3.back), ItemsGameObject.transform);

        i.setColor(data.colors[colorIdx]);
        i.setShape(data.shapes[shapeIdx]);

        //audioSource.Play();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        prepareInstance(1, 3, 0, 1, 20);
        base.Start();

        data = GameData.Instance;
        itemShapeGen = new GrowingRNG(data.shapes.Length, 1, 1, 20);
        itemColorGen = new GrowingRNG(data.colors.Length, 2, 1, 20);
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

    public float nextItemZOffset()
    {
        itemZOffset -= 1.0e-5f;
        return itemZOffset;
    }
}
