using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using TSTD;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "3f3f0817f7c23c3fc1b9d247c92300437af37a913760589a415a8336e7aeafff";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (msg) =>
        {
            int loopLength = msg.Length < names.Count ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, msg =>
        {
            GetLeaderboard();
        });
    }

    private void OnEnable()
    {
        EventManager.OnLeaderboardSubmit += SetLeaderboardEntry;
        //EventManager.GameOver += GetLeaderboard;
    }
    private void OnDisable()
    {
        EventManager.OnLeaderboardSubmit -= SetLeaderboardEntry;
        //EventManager.GameOver -= GetLeaderboard;
    }
}
