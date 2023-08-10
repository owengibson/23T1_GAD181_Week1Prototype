using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TenSecondsToDie
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private GameObject mainCanvas;
        [SerializeField] private GameObject endCanvas;
        [SerializeField] private TextMeshProUGUI winnerText;

        [ClientRpc]
        private void DisplayEndScreenClientRpc(Player winner)
        {
            mainCanvas.SetActive(false);
            endCanvas.SetActive(true);
            winnerText.text = $"Player {winner} wins!";
        }


        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnEnable()
        {
            EventManager.OnGameOver += DisplayEndScreenClientRpc;
        }
        private void OnDisable()
        {
            EventManager.OnGameOver -= DisplayEndScreenClientRpc;
        }
    }
}
