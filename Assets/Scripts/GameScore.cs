using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text totalTimeRemainingText;
    public TMP_Text eventTimeRemainingText;
    public int maxCombo = 10;

    public int Score { get; private set; }
    public int Combo { get; private set; }
    public float TimeLeft { get; private set; }

    public static GameScore Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void addScore(int score)
    {
        if (score <= 0)
        {
            Score += score * 10;
            Combo = 0;
        }
        else
        {
            Combo++;
            if (Combo > maxCombo)
                Combo = maxCombo;

            Score += score * 10 * Combo;
        }

        scoreText.SetText(Score.ToString());
        comboText.SetText("x" + Combo.ToString());
    }

    public void LateUpdate()
    {
        int totalTimeLeft = Mathf.CeilToInt(ClientGenerator.Instance.getRemainingTimeOfGame());
        totalTimeRemainingText.SetText((totalTimeLeft / 60) + "min " + (totalTimeLeft % 60 != 0 ? (totalTimeLeft % 60) + "s" : ""));
        eventTimeRemainingText.SetText(Mathf.CeilToInt(ClientGenerator.Instance.remainingDuration).ToString() + "s");
    }
}
