using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSavingController : MonoBehaviour
{
    public static ScoreSavingController Instance { get; private set; }
    public TMPro.TMP_InputField inputField;
    public TMPro.TextMeshPro scoreField;

    private float score;
   

    public void Awake()
    {
        Instance = this;
    }

    public void  SetScore(float s)
    {
        score = s;
        scoreField.SetText(score + " pts");
    }

    public void SaveScore()
    {

    }
}
