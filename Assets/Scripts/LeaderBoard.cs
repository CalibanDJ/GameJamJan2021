using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LeaderBoard
{
    public struct BoardItem
    {
        public int score { get; internal set; }
        public string name { get; internal set; }
    }

    private static List<BoardItem> _leaderBoard = new List<BoardItem>();
    private static List<BoardItem> safeLeaderBoard
    {
        get
        {
            if (_leaderBoard == null)
            {
                loadLeaderBoard();
            }

            return _leaderBoard;
        }
    }

    public static IReadOnlyList<BoardItem> LeaderBoardList => safeLeaderBoard;

    public static void addScore(int score, string name)
    {
        safeLeaderBoard.Add(new BoardItem { score = score, name = name });
        safeLeaderBoard.Sort();
        if (safeLeaderBoard.Count > 10)
        {
            safeLeaderBoard.RemoveAt(10);
        }

        PlayerPrefs.SetInt("LeaderBoard_Count", safeLeaderBoard.Count);
        for (int i = 0; i < safeLeaderBoard.Count; i++)
        {
            BoardItem item = safeLeaderBoard[i];
            PlayerPrefs.SetInt("LeaderBoard_Score_" + i, item.score);
            PlayerPrefs.SetString("LeaderBoard_Name_" + i, item.name);
        }
    }

    public static void loadLeaderBoard()
    {
        int count = PlayerPrefs.GetInt("LeaderBoard_Count");
        _leaderBoard = new List<BoardItem>(count);
        for (int i = 0; i < count; i++)
        {
            int score = PlayerPrefs.GetInt("LeaderBoard_Score_" + i);
            string name = PlayerPrefs.GetString("LeaderBoard_Name_" + i);
            _leaderBoard[i] = new BoardItem { score = score, name = name };
        }
    }
}
