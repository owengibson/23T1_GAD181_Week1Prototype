using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class Skull : MonoBehaviour
    {
        private float lifespan = 2.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !Timer.hasBypassKeyPressed)
            {
                EventManager.GameOver?.Invoke();
                Destroy(gameObject);
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
            }
        }
    }
}