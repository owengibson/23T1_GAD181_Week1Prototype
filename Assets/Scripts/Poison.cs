using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace TenSecondsToDie
{
    public class Poison : NetworkBehaviour
    {
        private float lifespan = 3.5f;
        private Pellet pelletType = Pellet.Poison;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //EventManager.OnPelletEaten?.Invoke("-1 second");
                EventManager.OnPelletEaten?.Invoke(other.GetComponent<PlayerController>().playerNum, pelletType);
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