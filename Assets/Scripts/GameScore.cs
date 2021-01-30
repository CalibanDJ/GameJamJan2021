using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text comboText;
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
}
