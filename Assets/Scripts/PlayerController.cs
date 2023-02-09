using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float xInput;
        [SerializeField] private float zInput;

        [SerializeField] private float movementSpeed = 5f;

        [SerializeField] private Rigidbody rb;

        private void Update()
        {
            xInput = Input.GetAxis("Horizontal");
            zInput = Input.GetAxis("Vertical");

            rb.AddForce(new Vector3(xInput, 0, zInput) * movementSpeed);
        }
    }
}