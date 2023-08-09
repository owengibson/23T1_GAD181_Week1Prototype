using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace TenSecondsToDie
{
    public class Poison : NetworkBehaviour
    {
        private float lifespan = 3.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //EventManager.OnPelletEaten?.Invoke("-1 second");
                EventManager.PoisonEaten?.Invoke(other.GetComponent<PlayerController>().playerNum);
                EventManager.OnPoisonDestroy?.Invoke();
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
                EventManager.OnPoisonDestroy?.Invoke();
            }
        }
    }
}