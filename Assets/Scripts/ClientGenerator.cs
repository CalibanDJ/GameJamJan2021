using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientGenerator : Generator
{
    public ItemGenerator itemGen;
    public Transform dialogParent;

    public Client clientPrefab;

    public WaitingLine[] wLines;
    public Sprite[] clientSprites;

    //Waves management
    public float rushHourDuration = 60; // in seconds
    public float chillHourDuration = 20; // in seconds
    public int nbRushHoursLeft = 3;
    public float remainingDuration;
    public bool inRushHour;

    public bool isRunning = true;

    public static ClientGenerator Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    private void switchPeriod() {
        remainingDuration = inRushHour ? chillHourDuration : rushHourDuration;
        inRushHour = !inRushHour;
    }

    private void moveClock() {
        remainingDuration -= Time.deltaTime;
        if (remainingDuration <= 0) {
            endPeriod();
        }
    }

    public float getRemainingTimeOfGame()
    {
        return remainingDuration + (!inRushHour ? rushHourDuration : 0)
            + (chillHourDuration + rushHourDuration) * ((nbRushHoursLeft - 1) < 0 ? 0 : (nbRushHoursLeft - 1));
    }

    private void setGeneratorForRushHour() {
        base.setNewTimer(0, 3, 0, 1, 10); // un mec toutes les 3 secondes au début puis rnged avec augmentation de 1

        Debug.Log("Rush Hour incoming");
    }

    private void setGeneratorForChillHour() {
        base.setNewTimer(5, 11, 0, 1, 10); // Un mec toutes les 5 a 15 secondes 
        itemSpawnRateGen.lockLeveling(); // Empeche le leveling du generateur

        Debug.Log("Chill Hour incoming");
    }

    private void endPeriod() {
        if (inRushHour) {
            nbRushHoursLeft--;
            // FOUTRE GENERATEUR PR CHILL HOUR
            setGeneratorForChillHour();
        }
        else {
            // FOUTRE GENERATEUR PR RUSH HOUR
            setGeneratorForRushHour();
        }

        if(nbRushHoursLeft <= 0) {
            // FIN DU JEU
            isRunning = false;
            PlayerPrefs.SetInt("CurrentScore", GameScore.Instance.Score);
            SceneManager.LoadScene("EndMenu");
            return;
        }
        switchPeriod();
    }

    private void assignDesiredItem(Client c) {
        int shapeIdx = Random.Range(0, itemGen.getShapePoolSize());
        int colorIdx = Random.Range(0, itemGen.getColorPoolSize());

        c.desiredShape = itemGen.getGameData().shapes[shapeIdx];
        c.desiredColor = itemGen.getGameData().colors[colorIdx];
    }

    private void assignWaitingLine(Client c, int idx) {
        c.setLine(wLines[idx]);
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
        int WLineIdx = findSmallestLineIdx();
        if(WLineIdx == -1) {
            // All lines are full
            return;
        }

        Client c = Instantiate(clientPrefab, new Vector2(0, 0), Quaternion.identity);
        c.dialogParent = dialogParent;
        c.setSprite(clientSprites[Random.Range(0, clientSprites.Length)]);
        assignDesiredItem(c);
        assignWaitingLine(c, WLineIdx);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        prepareInstance(5, 11, 0, 1, 10);
        base.Start();
        itemSpawnRateGen.lockLeveling();

        //Chill hour setting for starter
        inRushHour = false;
        remainingDuration = chillHourDuration;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isRunning) {
            base.Update();
            this.moveClock();
        }
    }
}
