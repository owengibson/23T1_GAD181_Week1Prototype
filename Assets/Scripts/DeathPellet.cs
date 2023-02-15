using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class DeathPellet : MonoBehaviour
    {
        private float lifespan = 2.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.GameOver?.Invoke();
            }
        }

        private void Update()
        {
            if (lifespan > 0)
            {
                lifespan -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
                EventManager.OnBadPelletDestroy?.Invoke();
            }
        }
    }
}