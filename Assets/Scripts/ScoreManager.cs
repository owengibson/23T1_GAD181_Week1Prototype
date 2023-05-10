using TSTD;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    private string scoreString;

    public void SubmitScore()
    {
        scoreString = GameManager.timeCounter.ToString("#0");
        EventManager.OnLeaderboardSubmit?.Invoke(inputName.text, int.Parse(scoreString));
    }
}
