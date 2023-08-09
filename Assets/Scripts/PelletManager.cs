using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace TenSecondsToDie
{
    public class PelletManager : NetworkBehaviour
    {
        /*private NetworkVariable<bool> isHeartActive = new(false);
        private NetworkVariable<bool> isPoisonActive = new(false);*/

        private bool isHeartActive = false;
        private bool isPoisonActive = false;
        private bool arePlayersFound = false;

        private float skullSpawnTimer = 0f;

        private Vector3 goodSpawnPos = new Vector3(10f, 10f, 10f);
        private Vector3 badSpawnPos = new Vector3(10f, 10f, 10f);
        private Vector3 skullSpawnPos = new Vector3(10f, 10f, 10f);

        private Transform[] players;

        [Header("Object References")]
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private GameObject poisonPrefab;
        [SerializeReference] private GameObject skullPrefab;

        [Space(15f)]
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private GameManager gameManager;

        [Header("Variables")]
        [SerializeField] float spawnDistanceFromPlayer = 2.5f;
        [SerializeField] float spawnDistanceFromPellets = 2f;
        [SerializeField] float velocityMultiplier = 0.25f;
        [SerializeField] float directionalityLimit = 5f;

        private void FindPlayers()
        {
            players = new Transform[2];

            for (int i = 0; i < 2; i++)
            {
               PlayerController[] playerControllers = FindObjectsOfType<PlayerController>();
                players[i] = playerControllers[i].transform;
            }
            arePlayersFound = true;
        }

        private void Update()
        {
            if (arePlayersFound)
            {
                if (!isHeartActive)
                {
                    // Good pellet spawning
                    GameObject spawnedHeart;
                    goodSpawnPos = SpawnPosCalculator();

                    spawnedHeart = Instantiate(heartPrefab, goodSpawnPos, Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0)));
                    spawnedHeart.GetComponent<NetworkObject>().Spawn(true);
                    isHeartActive = true;

                    audioManager.Play("SpawnGem");


                    // Bad pellet spawning
                    if (UnityEngine.Random.Range(1, 5) == 4 && !isPoisonActive)
                    {
                        GameObject spawnedPoison;
                        badSpawnPos = SpawnPosCalculator();
                        spawnedPoison = Instantiate(poisonPrefab, badSpawnPos, Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0));
                        spawnedPoison.GetComponent<NetworkObject>().Spawn(true);
                        isPoisonActive = true;

                        audioManager.Play("SpawnGem");
                    }
                }

                // Death pellet spawning
                skullSpawnTimer += Time.deltaTime;
                if (skullSpawnTimer >= 7.5f)
                {
                    GameObject spawnedSkull;

                    skullSpawnTimer = 0;
                    skullSpawnPos = SpawnPosCalculator();
                    spawnedSkull = Instantiate(skullPrefab, skullSpawnPos, Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0)));
                    spawnedSkull.GetComponent<NetworkObject>().Spawn(true);

                    audioManager.Play("SpawnGem");
                }
            }
        }

        private Vector3 SpawnPosCalculator()
        {
            float xSpawn;
            float zSpawn;
            Vector3 spawnPos;
            Vector2[] playerPos = new Vector2[players.Length];
            bool illegal = false;
            int spawnCounter = 0;
            Vector2[] directionSpawnLimit = new Vector2[players.Length];
            float[] directionality = new float[players.Length];

            do
            {
                xSpawn = UnityEngine.Random.Range(-5.8f, 5.8f);
                zSpawn = UnityEngine.Random.Range(-5.8f, 5.8f);
                spawnPos = new Vector3(xSpawn, 0.5f, zSpawn);
                for (int i = 0; i < players.Length; i++) playerPos[i] = new Vector2(players[i].position.x, players[i].position.z);
                for (int i = 0; i < players.Length; i++)
                {
                    Rigidbody rb = players[i].GetComponent<Rigidbody>();
                    directionSpawnLimit[i] = playerPos[i] + new Vector2(rb.velocity.x, rb.velocity.z) * velocityMultiplier;
                }
                for (int i = 0; i < players.Length; i++) directionality[i] = Vector2.Angle(spawnPos, directionSpawnLimit[i]);

                try
                {
                    spawnCounter++;
                    for (int i = 0; i < players.Length; i++)
                    {
                        // Must not be in path of player movement (scaling depending on velocity of player)
                        if (Vector2.Distance(new Vector2(xSpawn, zSpawn), playerPos[i]) < Vector2.Distance(playerPos[i], directionSpawnLimit[i]) && directionality[i] < directionalityLimit)
                        {
                            illegal = true;
                            Debug.Log("In the path of player");
                            throw new IllegalSpawnException();
                        }

                        // Must be far enough away from player
                        else if (Vector2.Distance(new Vector2(xSpawn, zSpawn), playerPos[i]) < spawnDistanceFromPlayer)
                        {
                            illegal = true;
                            Debug.Log("Too close to player");
                            throw new IllegalSpawnException();
                        }
                    }

                    // Must be inside spawn platform
                    if (Mathf.Abs(xSpawn) + Mathf.Abs(zSpawn) > 5.8f)
                    {
                        illegal = true;
                        Debug.Log("Outside of spawn platform");
                        throw new IllegalSpawnException();
                    }

                    // Must be far enough away from other pellets
                    else if (Vector3.Distance(spawnPos, goodSpawnPos) < spawnDistanceFromPellets || Vector3.Distance(spawnPos, badSpawnPos) < spawnDistanceFromPellets || Vector3.Distance(spawnPos, skullSpawnPos) < spawnDistanceFromPellets)
                    {
                        illegal = true;
                        Debug.Log("Too close to other pellets");
                        throw new IllegalSpawnException();
                    }
                    else illegal = false;
                }
                catch (IllegalSpawnException e)
                {
                    continue;
                }
            } while (illegal);
            Debug.Log(spawnCounter + " " + xSpawn + ", " + zSpawn);
            return spawnPos;
        }

        private void ResetActiveGoodPellets()
        {
            isHeartActive = false;
        }

        private void ResetActiveBadPellets()
        {
            isPoisonActive = false;
        }

        private void OnEnable()
        {
            EventManager.OnHeartDestroy += ResetActiveGoodPellets;
            EventManager.OnPoisonDestroy += ResetActiveBadPellets;
            EventManager.OnTwoPlayersConnected += FindPlayers;
        }
        private void OnDisable()
        {
            EventManager.OnHeartDestroy -= ResetActiveGoodPellets;
            EventManager.OnPoisonDestroy -= ResetActiveBadPellets;
            EventManager.OnTwoPlayersConnected -= FindPlayers;
        }
    }

    public class IllegalSpawnException : Exception
    {

    }
}