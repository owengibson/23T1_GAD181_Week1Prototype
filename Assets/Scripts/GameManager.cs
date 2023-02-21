using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chowen
{
    public class GameManager : MonoBehaviour
    {
        private float timeCounter = 0f;
        public static bool isGameActive = true;
        [SerializeField] private GameObject endScreen;
        [SerializeField] private TextMeshProUGUI endTimeText;

        private void Update()
        {
            if (isGameActive)
            {
                timeCounter += Time.deltaTime;
            }
        }

        private void EndScreen()
        {
            isGameActive = false;
            endScreen.SetActive(true);
            endTimeText.text = timeCounter.ToString("#0") + " seconds";

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