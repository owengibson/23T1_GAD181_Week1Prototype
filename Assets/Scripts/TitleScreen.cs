using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TenSecondsToDie
{
    public class TitleScreen : MonoBehaviour
    {
        [SerializeField] private GameObject mainCanvas;

        public void PlayGame()
        {
            gameObject.SetActive(false);
            mainCanvas.SetActive(true);
        }
    }
}