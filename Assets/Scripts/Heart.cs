using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace TenSecondsToDie
{
    public class Heart : NetworkBehaviour
    {
        private Pellet pelletType = Pellet.Heart;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //EventManager.OnPelletEaten?.Invoke("+1 second");
                EventManager.OnPelletEaten?.Invoke(other.GetComponent<PlayerController>().playerNum, pelletType);
                EventManager.OnHeartDestroy?.Invoke();
                NetworkObject.Despawn();
            }
        }
    }
}