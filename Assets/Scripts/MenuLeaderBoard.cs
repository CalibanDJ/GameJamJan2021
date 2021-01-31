using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLeaderBoard : MonoBehaviour
{
    public ScoreEntry scorePrefab;
    public Transform scoreTable;
    public Animator animator;
    public GameObject openObj;
    public GameObject closedObj;
    private bool open = false;

    public void OpenLeaderBoard()
    {
        open = !open;
        animator.SetBool("open", open);
    }

    public void startAnim()
    {
        closedObj.SetActive(!open);
    }

    public void endAnim()
    {
        openObj.SetActive(open);
        if (open)
        {
            while (scoreTable.childCount > 1)
            {
                Destroy(scoreTable.GetChild(1));
            }

            foreach (var sc in LeaderBoard.LeaderBoardList)
            {
                ScoreEntry entry = Instantiate(scorePrefab, scoreTable);
                entry.name.SetText(sc.name);
                entry.score.SetText(sc.score.ToString());
            }
        }
    }
}
