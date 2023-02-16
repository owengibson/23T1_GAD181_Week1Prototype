using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class PelletManager : MonoBehaviour
    {
        private float activeGoodPellets = 0f;
        private float activeBadPellets = 0f;

        private float deathPelletSpawnTimer = 0f;

        private Vector3 goodSpawnPos = new Vector3(10f, 10f, 10f);
        private Vector3 badSpawnPos = new Vector3(10f, 10f, 10f);
        private Vector3 deathSpawnPos = new Vector3(10f, 10f, 10f);


        [Header("Object References")]
        [SerializeField] private Transform player;
        [SerializeField] private GameObject goodPelletPrefab;
        [SerializeField] private GameObject badPelletPrefab;
        [SerializeReference] private GameObject deathPelletPrefab;

        [Header("Variables")]
        [SerializeField] float spawnDistanceFromPlayer = 2.5f;

        private void Update()
        {
            if (activeGoodPellets == 0)
            {
                // Good pellet spawning
                goodSpawnPos = SpawnPosCalculator();
                
                Instantiate(goodPelletPrefab, goodSpawnPos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
                activeGoodPellets += 1;

                // Bad pellet spawning
                if (Random.Range(1,5) == 4 && activeBadPellets == 0)
                {
                    badSpawnPos = SpawnPosCalculator();
                    Instantiate(badPelletPrefab, badSpawnPos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
                    activeBadPellets += 1;
                }
            }
            deathPelletSpawnTimer += Time.deltaTime;
            if (deathPelletSpawnTimer >= 5f)
            {
                deathPelletSpawnTimer = 0;
                deathSpawnPos = SpawnPosCalculator();
                Instantiate(deathPelletPrefab, deathSpawnPos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
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

                if (Mathf.Abs(xSpawn) + Mathf.Abs(zSpawn) > 5.8f)
                {
                    illegal = true;
                }
                else if (Vector2.Distance(new Vector2(xSpawn, zSpawn), new Vector2(player.position.x, player.position.z)) < spawnDistanceFromPlayer)
                {
                    illegal = true;
                }
                else if (Vector3.Distance(spawnPos, goodSpawnPos) < 1f || Vector3.Distance(spawnPos, badSpawnPos) < 1f || Vector3.Distance(spawnPos, deathSpawnPos) < 1f)
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
            activeGoodPellets = 0;
        }

        private void ResetActiveBadPellets()
        {
            activeBadPellets = 0;
        }

        private void OnEnable()
        {
            EventManager.OnGoodPelletDestroy += ResetActiveGoodPellets;
            EventManager.OnBadPelletDestroy += ResetActiveBadPellets;
        }
        private void OnDisable()
        {
            EventManager.OnGoodPelletDestroy -= ResetActiveGoodPellets;
            EventManager.OnBadPelletDestroy -= ResetActiveBadPellets;
        }
    }
}