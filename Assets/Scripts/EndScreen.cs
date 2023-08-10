using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TenSecondsToDie
{
    public class EndScreen : NetworkBehaviour
    {
        [SerializeField] private GameObject mainCanvas;
        [SerializeField] private GameObject endCanvas;
        [SerializeField] private TextMeshProUGUI winnerText;
        [SerializeField] private AudioManager audioManager;

        [ClientRpc]
        public void DisplayEndScreenClientRpc(Player winner)
        {
            audioManager.SwitchToEndScreenSoundtrack();
            mainCanvas.SetActive(false);
            endCanvas.SetActive(true);
            winnerText.text = $"Player {winner} wins!";
        }

        //[ClientRpc]
        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            NetworkManager.Singleton.Shutdown();
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
