using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TenSecondsToDie
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