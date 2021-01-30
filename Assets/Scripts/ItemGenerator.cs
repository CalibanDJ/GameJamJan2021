using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemGen;

public class ItemGenerator : MonoBehaviour
{
    private GrowingRNG itemShapeGen;
    private GrowingRNG itemColorGen;

    // Spawn rate
    private GrowingRNG itemSpawnRateGen;
    // This should be atleast 1
    private int minSecond;
    //
    private int maxSecondDelay;
    private float timeBeforeNextSpawn;

    private Vector2 spawnPosition;
    private GameData data;

    public Item itemPrefab;

    // DEFAULT : startPool is half of pool, Step is 1/10
    public ItemGenerator(GameData gd) {
        data = gd;
        itemShapeGen = new GrowingRNG(gd.shapes.Length, gd.shapes.Length / 2, gd.shapes.Length /10);
        itemColorGen = new GrowingRNG(gd.colors.Length, gd.colors.Length / 2, gd.colors.Length /10);

        // DEFAULT Parameter :
        minSecond = 1;
        maxSecondDelay = 10;
        itemSpawnRateGen = new GrowingRNG(maxSecondDelay, 4, 1); // First parameter + minSecond is the biggest interval between two spawns
                                                                 // First - (Second parameter - 1) is the initial smallest interval between two spawns
                                                                 // Third parameter is the step. At each level, the smallest interval is lowered by this parameter.
        generateNewTimer();
    }

    // Set a Timer between minSecond and minSecond + maxSecondDelay
    private void generateNewTimer() {
        timeBeforeNextSpawn = minSecond + maxSecondDelay - itemSpawnRateGen.generateNumber();
    }

    public void generate() {
        int shapeIdx = itemShapeGen.generateNumber();
        int colorIdx = itemColorGen.generateNumber();

        Item i = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        i.setColor(data.colors[colorIdx]);
        i.setShape(data.shapes[shapeIdx]);
    }

    public void setSpawnPosition(Vector2 newPosition) {
        spawnPosition = newPosition;
    }

    private void moveClock() {
        timeBeforeNextSpawn -= Time.deltaTime;
        if(timeBeforeNextSpawn <= 0) {
            this.generate();
            generateNewTimer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Au cas ou jsp trop si le constructeur sert
        itemShapeGen = new GrowingRNG(data.shapes.Length, data.shapes.Length / 2, data.shapes.Length /10);
        itemColorGen = new GrowingRNG(data.colors.Length, data.colors.Length / 2, data.colors.Length /10);
    }

    // Update is called once per frame
    void Update()
    {
        moveClock();
    }
}
