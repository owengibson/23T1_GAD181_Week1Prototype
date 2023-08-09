using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace TenSecondsToDie
{
    public class Skull : NetworkBehaviour
    {
        private float lifespan = 2.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.GameOver?.Invoke();
                NetworkObject.Despawn();
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
                NetworkObject.Despawn();
            }
        }
    }
}