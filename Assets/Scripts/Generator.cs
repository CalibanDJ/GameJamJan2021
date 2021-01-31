using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemGen;

public abstract class Generator : MonoBehaviour
{
    // Spawn rate
    public GrowingRNG itemSpawnRateGen;
    // This should be atleast 1
    private int minSecond = 0;
    private int maxSecondDelay = 6;
    private int step = 2;
    private int startTime = 0;
    private int growThreshold = 1;

    public float timeBeforeNextSpawn;

    private void moveClock() {
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

    protected void prepareInstance(int minScd, int maxScd, int startcd, int step, int growThreshold) {
        // DEFAULT Parameter :
        minSecond = minScd;
        maxSecondDelay = maxScd;
        startTime = startcd;
        this.step = step;
        this.growThreshold = growThreshold;
    }

    protected void setNewTimer(int minScd, int maxScd, int startcd, int step, int growThreshold) {
        prepareInstance(minScd, maxScd, startcd, step, growThreshold);
        
        // First parameter + minSecond is the biggest interval between two spawns
        // First - (Second parameter - 1) is the initial smallest interval between two spawns
        // Third parameter is the step. At each level, the smallest interval is lowered by this parameter.
        itemSpawnRateGen = new GrowingRNG(maxSecondDelay, startTime, step, this.growThreshold);

        generateNewTimer();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        itemSpawnRateGen = new GrowingRNG(maxSecondDelay, startTime, step); // First parameter + minSecond is the biggest interval between two spawns
                                                                            // First - (Second parameter - 1) is the initial smallest interval between two spawns
                                                                            // Third parameter is the step. At each level, the smallest interval is lowered by this parameter.
        generateNewTimer();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        moveClock();
    }
}
