using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class BadPellet : MonoBehaviour
    {
        private float lifespan = 3.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.OnPelletEaten?.Invoke("-1 second");
                EventManager.BadPelletEaten?.Invoke(1);
                EventManager.OnBadPelletDestroy?.Invoke();
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
                EventManager.OnBadPelletDestroy?.Invoke();
            }
        }
    }
}