using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chowen
{
    public class Timer : MonoBehaviour
    {
        public float timeRemaining = 10f;
        private TextMeshProUGUI timerText;
        [SerializeField] private AudioManager audioManager;

        private void Start()
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }
        private void Update()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = timeRemaining.ToString("00.00");
            }
            else
            {
                //EventManager.GameOver?.Invoke();
            }

            if (GameManager.isGameActive == true)
            {
                TimerSound();
            }
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