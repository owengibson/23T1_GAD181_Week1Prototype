using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class PelletManager : MonoBehaviour
    {
        private bool isHeartActive = false;
        private bool isPoisonActive = false;

        private float skullSpawnTimer = 0f;

        private Vector3 goodSpawnPos = new Vector3(10f, 10f, 10f);
        private Vector3 badSpawnPos = new Vector3(10f, 10f, 10f);
        private Vector3 skullSpawnPos = new Vector3(10f, 10f, 10f);


        [Header("Object References")]
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private GameObject poisonPrefab;
        [SerializeReference] private GameObject skullPrefab;

        [Space(15f)]
        [SerializeField] private Transform player;
        [SerializeField] private AudioManager audioManager;

        [Header("Variables")]
        [SerializeField] float spawnDistanceFromPlayer = 2.5f;
        [SerializeField] float spawnDistanceFromPellets = 2f;

        private void Update()
        {
            if (!isHeartActive)
            {
                // Good pellet spawning
                goodSpawnPos = SpawnPosCalculator();
                
                Instantiate(heartPrefab, goodSpawnPos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
                isHeartActive = true;

                audioManager.Play("SpawnGem");

                // Bad pellet spawning

                if (Random.Range(1, 5) == 4 && !isPoisonActive)
                {
                    badSpawnPos = SpawnPosCalculator();
                    Instantiate(poisonPrefab, badSpawnPos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
                    isPoisonActive = true;

                    audioManager.Play("SpawnGem");
                }

            }
            // Death pellet spawning
            skullSpawnTimer += Time.deltaTime;
            if (skullSpawnTimer >= 5f && GameManager.isGameActive)
            {
                skullSpawnTimer = 0;
                skullSpawnPos = SpawnPosCalculator();
                Instantiate(skullPrefab, skullSpawnPos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));

                audioManager.Play("SpawnGem");
            }
        }

        private Vector3 SpawnPosCalculator()
        {
            float xSpawn;
            float zSpawn;
            Vector3 spawnPos;
            bool illegal = false;
            int spawnCounter = 0;
            do
            {
                xSpawn = Random.Range(-5.8f, 5.8f);
                zSpawn = Random.Range(-5.8f, 5.8f);
                spawnPos = new Vector3(xSpawn, 0.5f, zSpawn);

                spawnCounter++;

                if (Mathf.Abs(xSpawn) + Mathf.Abs(zSpawn) > 5.8f) // Must be inside spawn platform
                {
                    illegal = true;
                }
                else if (Vector2.Distance(new Vector2(xSpawn, zSpawn), new Vector2(player.position.x, player.position.z)) < spawnDistanceFromPlayer) // Must be far enough away from player
                {
                    illegal = true;
                }
                else if (Vector3.Distance(spawnPos, goodSpawnPos) < spawnDistanceFromPellets || Vector3.Distance(spawnPos, badSpawnPos) < spawnDistanceFromPellets || Vector3.Distance(spawnPos, skullSpawnPos) < spawnDistanceFromPellets) // Must be far enough away from other pellets
                {
                    illegal = true;
                }
                else illegal = false;

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
        }
        private void OnDisable()
        {
            EventManager.OnHeartDestroy -= ResetActiveGoodPellets;
            EventManager.OnPoisonDestroy -= ResetActiveBadPellets;
        }
    }
}