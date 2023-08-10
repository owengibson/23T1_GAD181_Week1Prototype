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

        public NetworkVariable<int> playerOneScore = new(5);

        [SerializeField] private TextMeshProUGUI playerOneText;
        [SerializeField] private TextMeshProUGUI playerTwoText;
        [SerializeField] private Slider scoreSlider;

        private int playerTwoScore = 5;

        [ContextMenu("Add Score")]
        private void UpdateScore(Player player, Pellet pelletType)
        {
            //if (!NetworkManager.Singleton.IsHost) return;

            if (pelletType == Pellet.Heart)
            {
                switch (player)
                {
                    case Player.One:
                        playerOneScore.Value++;
                        playerTwoScore--;
                        break;

                    case Player.Two:
                        playerTwoScore++;
                        playerOneScore.Value--;
                        break;
                }
            }
            else if (pelletType == Pellet.Skull)
            {
                switch (player)
                {
                    case Player.One:
                        playerOneScore.Value--;
                        playerTwoScore++;
                        break;

                    case Player.Two:
                        playerTwoScore--;
                        playerOneScore.Value++;
                        break;
                }
            }
            else Debug.Log("Invalid pellet type for score update.");

            playerOneText.text = playerOneScore.Value.ToString();
            playerTwoText.text = playerTwoScore.ToString();
            StartCoroutine(LerpSlider(playerOneScore.Value, 0.15f));
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