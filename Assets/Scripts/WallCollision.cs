using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class WallCollision : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;

        private void OnCollisionEnter(Collision collision)
        {
            audioManager.Play("WallCollision");
        }
    }
}