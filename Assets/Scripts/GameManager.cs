using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chowen
{
    public class GameManager : MonoBehaviour
    {
        private float timeCounter = 15f;
        public static bool isGameActive = true;
        private bool endSoundPlayed = false;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private TextMeshProUGUI endTimeText;
        [SerializeField] private TextMeshProUGUI startCountdownTimer;
        [SerializeField] private AudioManager audioManager;

        private void Start()
        {
            //StartCoroutine(Countdown(3));
        }

        private void Update()
        {
            if (isGameActive)
            {
                timeCounter += Time.deltaTime;
            }
        }

        //IEnumerator Countdown(int seconds)
        //{
        //    int count = seconds;

        //    while (count > 0)
        //    {
        //        startCountdownTimer.text = count.ToString();
        //        // display something...
        //        yield return new WaitForSeconds(1);
        //        count--;
        //    }

        //    // count down is finished...
        //    startCountdownTimer.text = "";
        //    isGameActive = true;
        //}

        private void EndScreen()
        {
            isGameActive = false;
            endScreen.SetActive(true);
            endTimeText.text = timeCounter.ToString("#0") + " seconds";
            if (endSoundPlayed == false)
            {
                audioManager.Play("EndSound");
                endSoundPlayed = true;
            }
            
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnEnable()
        {
            EventManager.GameOver += EndScreen;
        }
        private void OnDisable()
        {
            EventManager.GameOver -= EndScreen;
        }
    }
}