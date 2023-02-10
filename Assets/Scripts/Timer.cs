using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OwenGibson
{
    public class Timer : MonoBehaviour
    {
        public float timeRemaining = 10f;
        private TextMeshProUGUI timerText;

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
            EventManager.GoodPelletEaten += AddTime;
            EventManager.BadPelletEaten += SubtractTime;
        }
        private void OnDisable()
        {
            EventManager.GoodPelletEaten -= AddTime;
            EventManager.BadPelletEaten -= SubtractTime;
        }
    }
}