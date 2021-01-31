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

    public void Start()
    {
        while (scoreTable.childCount > 1)
        {
            Destroy(scoreTable.GetChild(1));
        }

        int rank = 1;
        foreach (var sc in LeaderBoard.LeaderBoardList)
        {
            ScoreEntry entry = Instantiate(scorePrefab, scoreTable);
            entry.nameVal.SetText(rank + ". " + sc.name + " " + sc.score.ToString());
            //entry.score.SetText(sc.score.ToString());
            rank++;
        }
    }

    public void OpenLeaderBoard()
    {
        open = !open;
        animator.SetBool("open", open);
    }

    public void startAnim()
    {
        closedObj.SetActive(!open);
        if (open)
            transform.SetAsLastSibling();
        else
            transform.SetSiblingIndex(transform.parent.childCount - 2);
    }

    public void endAnim()
    {
        openObj.SetActive(open);
    }
}
