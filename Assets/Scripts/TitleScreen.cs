using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chowen
{
    public class TitleScreen : MonoBehaviour
    {
        private AudioSource soundtrack;

        private void Start()
        {
            soundtrack = GetComponent<AudioSource>();
            soundtrack.Play();
        }
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}