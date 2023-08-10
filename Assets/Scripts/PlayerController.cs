using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace TenSecondsToDie
{
    public enum Player { One, Two }

    public class PlayerController : NetworkBehaviour
    {
        private GameManager _gameManager;

        public Player playerNum;

        [SerializeField] private float xInput;
        [SerializeField] private float zInput;
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float velocityMultiplier;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private Material playerTwoMaterial;

        public override void OnNetworkSpawn()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _gameManager.connectedPlayers.Add(gameObject);
            if (_gameManager.connectedPlayers.Count == 1)
            {
                playerNum = Player.One;
                gameObject.name = "Player1";
                transform.position = new Vector3(-0.8f, 1.077f, 0);
            }
            else if (_gameManager.connectedPlayers.Count == 2)
            {
                playerNum = Player.Two;
                gameObject.name = "Player2";
                GetComponent<MeshRenderer>().material = playerTwoMaterial;
                transform.position = new Vector3(0.8f, 1.077f, 0);
                EventManager.OnTwoPlayersConnected?.Invoke();
            }
            else Destroy(gameObject);

            base.OnNetworkSpawn();
        }

        public override void OnNetworkDespawn()
        {
            EventManager.OnPlayerTwoDisconnect?.Invoke();
            _gameManager.connectedPlayers.Remove(gameObject);

            base.OnNetworkDespawn();
        }

        private void FixedUpdate()
        {
            if(_gameManager.connectedPlayers.Count == 2)
            {
                xInput = Input.GetAxis("Horizontal");
                zInput = Input.GetAxis("Vertical");

                rb.AddForce(new Vector3(xInput, 0, zInput) * movementSpeed);
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, transform.position + rb.velocity * velocityMultiplier);
        }
    }
}