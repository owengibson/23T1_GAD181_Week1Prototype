using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSTD
{
    public class Heart : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.OnPelletEaten?.Invoke("+1 second");
                EventManager.HeartEaten?.Invoke(1);
                EventManager.OnHeartDestroy?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}