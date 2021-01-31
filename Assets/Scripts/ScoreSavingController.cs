using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSavingController : MonoBehaviour
{
    public static ScoreSavingController Instance { get; private set; }
    public TMPro.TMP_InputField inputField;
    public TMPro.TMP_Text scoreField;

    private float score;
   

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        SetScore(PlayerPrefs.GetInt("CurrentScore"));
    }

    public void  SetScore(float s)
    {
        score = s;
        scoreField.SetText(score + " pts");
    }

    public void SaveScore()
    {
        LeaderBoard.addScore((int)score, inputField.text);
    }
}
