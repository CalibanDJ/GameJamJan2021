using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor : MonoBehaviour
{
    public Vector2[] positions;
    public Sprite[] sprites;
    public GameObject standingObj;
    public GameObject movingObj;
    public SpriteRenderer standingRenderer;
    public Transform startPoint;
    public Transform endPoint;

    public float timer;
    public float durationTillDoomed = 5;
    private bool preparingToAttack = false;
    private bool doomActivated = false;
    public bool isTriggered => preparingToAttack || doomActivated;

    public static Janitor Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    private void moveClock() {
        timer += Time.deltaTime;

        if(timer >= durationTillDoomed) {
            attackPlayer();
            return;
        }
        updateStandingPosition();
    }

    private void updateStandingPosition() {
        int positionIdx = ((int) (timer/durationTillDoomed) ) * positions.Length;
        standingObj.transform.position = positions[positionIdx];
        this.standingRenderer.sprite = sprites[positionIdx];
    }

    public void setPreparingToAttack()
    {
        if (!isTriggered)
        {
            preparingToAttack = true;
            standingObj.SetActive(true);
        }
    }

    // TODO
    private void attackPlayer() {
        preparingToAttack = false; // is attacking
        standingObj.SetActive(false);

        movingObj.SetActive(true);
        movingObj.transform.position = startPoint.position;
        doomActivated = true;

        // FAIRE LE TRUC DU BULLDOZER
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(preparingToAttack) {
            moveClock();
        }
        if (doomActivated)
        {
            if (movingObj.transform.position.x <= endPoint.position.x)
            {
                movingObj.SetActive(false);
                doomActivated = false;
            }
        }
    }
}
