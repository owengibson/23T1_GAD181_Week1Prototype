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
        private Player playerCol;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //EventManager.OnPelletEaten?.Invoke("-1 second");
                
                playerCol = other.GetComponent<PlayerController>().playerNum;

                DespawnPelletServerRpc(playerCol);
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
                EventManager.OnPoisonDestroy?.Invoke();
                NetworkObject.Despawn();
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnPelletServerRpc(Player player)
        {
            EventManager.OnPelletEaten?.Invoke(playerCol, pelletType);
            EventManager.OnPoisonDestroy?.Invoke();
            NetworkObject.Despawn();
        }
    }
}