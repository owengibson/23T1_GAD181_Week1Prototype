using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class GoodPellet : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.OnPelletEaten?.Invoke("+1 second");
                EventManager.GoodPelletEaten?.Invoke(1);
                EventManager.OnGoodPelletDestroy?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}