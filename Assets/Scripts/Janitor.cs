using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janitor : MonoBehaviour
{
    public Vector2[] positions;
    public Sprite[] sprites;

    public float timer;
    public float durationTillDoomed = 5;
    private bool preparingToAttack = true;

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
        this.transform.position = positions[positionIdx];
    }

    // TODO
    private void attackPlayer() {
        preparingToAttack = false; // is attacking

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
    }
}
