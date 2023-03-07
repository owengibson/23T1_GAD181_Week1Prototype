using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chowen
{
    public class Timer : MonoBehaviour
    {
        public static float timeRemaining;
        public static bool startGameCountdown = false;
        private TextMeshProUGUI timerText;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private TextMeshProUGUI startCountdownTimer;
        private bool hasBypassKeyPressed = false;

        private void Start()
        {
            startGameCountdown = false;
            timerText = GetComponent<TextMeshProUGUI>();
            timeRemaining = 13.5f;
        }
        private void Update()
        {
            TimerSound();
            InitialCoundownTimer();

            if (timeRemaining > 0 && !GameManager.hasGameEnded)
            {
                timeRemaining -= Time.deltaTime;

                if (timeRemaining > 10)
                {
                    if (!startGameCountdown)
                    {
                        //this is the start countdown timer
                        startCountdownTimer.text = (timeRemaining - 10).ToString("0.");
                    }
                    else
                    {
                        startGameCountdown = true;
                        timerText.text = timeRemaining.ToString("00.00");
                    }
                }
                else
                {
                    GameManager.isGameActive = true;
                    if (!audioManager.mainStart.source.isPlaying)
                    {
                        audioManager.Play("MainStart");
                        audioManager.Play("DeathScreen");
                    }
                    //this is the game timer
                    timerText.text = timeRemaining.ToString("00.00");
                    //this is the start countdown timer
                    startCountdownTimer.text = "";
                    startGameCountdown = true;
                }

                
            }
            else
            {

                if (!hasBypassKeyPressed) EventManager.GameOver?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha0)) hasBypassKeyPressed = !hasBypassKeyPressed;

            //if (GameManager.isGameActive == true)
            //{
            //    TimerSound();
            //}
        }

        private void TimerSound()
        {
            if (timeRemaining <= 3.1 && timeRemaining >= 3)
            {
                audioManager.Play("3");
                Debug.Log("3");
            }
            else if (timeRemaining <= 2.1 && timeRemaining >= 2)
            {
                audioManager.Play("2");
            }
            else if (timeRemaining <= 1.1 && timeRemaining >= 1)
            {
                audioManager.Play("1");
            }
            else if (timeRemaining <= 0.001 && timeRemaining >= 0)
            {
                audioManager.Play("0");
            }
        }

        private void InitialCoundownTimer()
        {
            if (timeRemaining == 13.5 || timeRemaining <= 12.71 && timeRemaining >= 12.7 || timeRemaining <= 11.71 && timeRemaining >= 11.7)
            {
                audioManager.Play("Tick");
                Debug.Log("Tick");
            }
            else if (timeRemaining <= 10.71 && timeRemaining >= 10.7)
            {
                audioManager.Play("Tick2");
                Debug.Log("Tick2");
            }
        }
        private void AddTime(float time)
        {
            timeRemaining += time;
        }

        private void SubtractTime(float time)
        {
            timeRemaining -= time;
        }

        private void OnEnable()
        {
            EventManager.HeartEaten += AddTime;
            EventManager.BadPelletEaten += SubtractTime;
        }
        private void OnDisable()
        {
            EventManager.HeartEaten -= AddTime;
            EventManager.BadPelletEaten -= SubtractTime;
        }
    }
}