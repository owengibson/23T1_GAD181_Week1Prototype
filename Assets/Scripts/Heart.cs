using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace TenSecondsToDie
{
    public class Heart : NetworkBehaviour
    {
        private Pellet pelletType = Pellet.Heart;
        private Player playerCol;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //EventManager.OnPelletEaten?.Invoke("+1 second");
                
                playerCol = other.GetComponent<PlayerController>().playerNum;

                Debug.Log("Heart pellet picked up by " + other.name);
                DespawnPelletServerRpc(playerCol);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnPelletServerRpc(Player player)
        {
            EventManager.OnPelletEaten?.Invoke(player, pelletType);
            EventManager.OnHeartDestroy?.Invoke();
            NetworkObject.Despawn();
        }
    }
}