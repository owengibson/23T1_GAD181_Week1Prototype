using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace TenSecondsToDie
{
    public class Heart : NetworkBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //EventManager.OnPelletEaten?.Invoke("+1 second");
                EventManager.HeartEaten?.Invoke(other.GetComponent<PlayerController>().playerNum);
                EventManager.OnHeartDestroy?.Invoke();
                NetworkObject.Despawn();
            }
        }
    }
}