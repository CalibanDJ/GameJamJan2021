using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class purpose is to detect the number of items on the floor to summon the great and mighty Janny the Janitor

public class Floor : MonoBehaviour
{
    public int nbItems;

    private float initialTimerValue = 5;
    private float timerValue;

    private int oddsScale = 1000;

    private void moveClock() {
        timerValue -= Time.deltaTime;
        if(timerValue <= 0) {
            rollForJanitor();
            setTimer();
        }
    }

    private void setTimer() {
        timerValue = initialTimerValue;
    }

    private void rollForJanitor() {
        int odds = evaluateSpawningOdds();

        int roll = Random.Range(0, oddsScale);

        if( roll < odds) {
            // SUMMON THE JANITOR
        }
    }

    private int evaluateSpawningOdds() {
        return nbItems*10 > oddsScale ? oddsScale : nbItems*10;
    }

    // Start is called before the first frame update
    void Start()
    {
        setTimer();
    }

    // Update is called once per frame
    void Update()
    {
        moveClock();
    }

    void OnCollisionEnter2D(Collision2D other) {
        nbItems++;
    }

    void  OnCollisionExit2D(Collision2D other) {
        nbItems--;
    }
}
