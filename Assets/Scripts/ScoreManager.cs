using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsToDie
{
    public class ScoreManager : NetworkBehaviour
    {
        /*[SerializeField] private TMP_InputField inputName;
        private string scoreString;*/

        //public NetworkVariable<int> playerOneScore = new(5);

        [SerializeField] private TextMeshProUGUI playerOneText;
        [SerializeField] private TextMeshProUGUI playerTwoText;
        [SerializeField] private Slider scoreSlider;

        private int playerOneScore = 5;
        private int playerTwoScore = 5;

        [ContextMenu("Add Score")]
        private void UpdateScore(Player player, Pellet pelletType)
        {
            if (!NetworkManager.Singleton.IsHost) return;

            if (pelletType == Pellet.Heart)
            {
                switch (player)
                {
                    case Player.One:
                        playerOneScore++;
                        playerTwoScore--;
                        break;

                    case Player.Two:
                        playerTwoScore++;
                        playerOneScore--;
                        break;
                }
            }
            else if (pelletType == Pellet.Poison)
            {
                switch (player)
                {
                    case Player.One:
                        playerOneScore--;
                        playerTwoScore++;
                        break;

                    case Player.Two:
                        playerTwoScore--;
                        playerOneScore++;
                        break;
                }
            }
            else Debug.Log("Invalid pellet type for score update.");

            UpdateScoreUIClientRpc(playerOneScore, playerTwoScore);

            if (playerOneScore == 10)
            {
                // PLAYER ONE WINS
                EventManager.OnGameOver?.Invoke(Player.One);
            }
            else if (playerOneScore == 0)
            {
                // PLAYER TWO WINS
                EventManager.OnGameOver?.Invoke(Player.Two);
            }
        }

        [ClientRpc]
        private void UpdateScoreUIClientRpc(int player1, int player2)
        {
            playerOneText.text = player1.ToString();
            playerTwoText.text = player2.ToString();
            StartCoroutine(LerpSlider(player1, 0.15f));
            //scoreSlider.value = Convert.ToSingle(playerOneScore.Value);
        }

        private IEnumerator LerpSlider(int target, float time)
        {
            float counter = 0f;
            float start = scoreSlider.value;

            while (counter < time)
            {
                counter += Time.deltaTime;
                scoreSlider.value = Mathf.Lerp(start, target, counter / time);
                yield return null;
            }
        }

        /*public void SubmitScore()
        {
            scoreString = GameManager.timeCounter.ToString("#0");
            EventManager.OnLeaderboardSubmit?.Invoke(inputName.text, int.Parse(scoreString));
        }*/

        private void OnEnable()
        {
            EventManager.OnPelletEaten += UpdateScore;
        }
        private void OnDisable()
        {
            EventManager.OnPelletEaten -= UpdateScore;
        }
    }
}