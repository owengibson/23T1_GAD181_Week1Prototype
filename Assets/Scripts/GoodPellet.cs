using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class GoodPellet : MonoBehaviour
    {
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.GoodPelletEaten?.Invoke(1);
                EventManager.OnPelletDestroy?.Invoke();
                Destroy(gameObject);
            }
            if(!other.CompareTag("Pellet Spawn Area"))
            {
                EventManager.OnPelletDestroy?.Invoke();
                Destroy(gameObject);
            }
            else meshRenderer.enabled = true;
        }
    }
}