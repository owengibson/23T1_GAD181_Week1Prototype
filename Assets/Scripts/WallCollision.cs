using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSTD
{
    public class WallCollision : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;

        private void OnCollisionEnter(Collision collision)
        {
            if (GameManager.isGameActive)
            {
                audioManager.Play("WallCollision");
            }
            
        }
    }
}