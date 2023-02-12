using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class PelletManager : MonoBehaviour
    {
        private float activeGoodPellets = 0f;
        private float activeBadPellets = 0f;

        private float deathPelletSpawnTimer = 0f;

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
                Vector3 spawnPos = SpawnPosCalculator();
                
                Instantiate(goodPelletPrefab, spawnPos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
                activeGoodPellets += 1;

                // Bad pellet spawning
                if (Random.Range(1,5) == 4 && activeBadPellets == 0)
                {
                    Vector3 badSpawnPos = SpawnPosCalculator();
                    Instantiate(badPelletPrefab, badSpawnPos, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
                    activeBadPellets += 1;
                }
            }
            deathPelletSpawnTimer += Time.deltaTime;
            if (deathPelletSpawnTimer >= 5f)
            {
                deathPelletSpawnTimer = 0;
                Vector3 deathSpawnPos = SpawnPosCalculator();
                Instantiate(deathPelletPrefab, deathSpawnPos, Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
            }
        }

        private Vector3 SpawnPosCalculator()
        {
            float xSpawn;
            float zSpawn;
            bool illegal = false;
            int spawnCounter = 0;
            do
            {
                xSpawn = Random.Range(-5.8f, 5.8f);
                zSpawn = Random.Range(-5.8f, 5.8f);

                spawnCounter++;

                if (Mathf.Abs(xSpawn) + Mathf.Abs(zSpawn) > 5.8f)
                {
                    illegal = true;
                }
                else if (Vector2.Distance(new Vector2(xSpawn, zSpawn), new Vector2(player.position.x, player.position.z)) < spawnDistanceFromPlayer)
                {
                    illegal = true;
                }
                else illegal = false;

            } while (illegal);
            Debug.Log(spawnCounter + " " + xSpawn + ", " + zSpawn);
            return new Vector3(xSpawn, 0.5f, zSpawn);
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