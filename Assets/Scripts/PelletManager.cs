using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class PelletManager : MonoBehaviour
    {
        private float activeGoodPellets = 0f;
        [SerializeField] private GameObject goodPelletGO;
        [SerializeField] private GameObject badPelletGO;

        private void Update()
        {
            if (activeGoodPellets == 0)
            {
                Instantiate(goodPelletGO, new Vector3(Random.Range(-6,6), 0.5f, Random.Range(-6,6)), Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)));
                activeGoodPellets += 1;
            }
        }

        private void ResetActiveGoodPellets()
        {
            activeGoodPellets = 0;
        }

        private void OnEnable()
        {
            EventManager.OnPelletDestroy += ResetActiveGoodPellets;
        }
        private void OnDisable()
        {
            EventManager.OnPelletDestroy -= ResetActiveGoodPellets;
        }
    }
}