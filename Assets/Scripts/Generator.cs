using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemGen;

public abstract class Generator : MonoBehaviour
{

    // Spawn rate
    private GrowingRNG itemSpawnRateGen;
    // This should be atleast 1
    private int minSecond;
    private int maxSecondDelay;
    private float timeBeforeNextSpawn;

    protected void moveClock() {
        timeBeforeNextSpawn -= Time.deltaTime;
        if(timeBeforeNextSpawn <= 0) {
            this.generate();
            generateNewTimer();
        }
    }

    // Set a Timer between minSecond and minSecond + maxSecondDelay
    private void generateNewTimer() {
        timeBeforeNextSpawn = minSecond + maxSecondDelay - itemSpawnRateGen.generateNumber();
    }

    public abstract void generate();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // DEFAULT Parameter :
        minSecond = 1;
        maxSecondDelay = 10;
        itemSpawnRateGen = new GrowingRNG(maxSecondDelay, 4, 1); // First parameter + minSecond is the biggest interval between two spawns
                                                                 // First - (Second parameter - 1) is the initial smallest interval between two spawns
                                                                 // Third parameter is the step. At each level, the smallest interval is lowered by this parameter.
        generateNewTimer();
    }

    // Update is called once per frame
    void Update()
    {
        moveClock();
    }
}
