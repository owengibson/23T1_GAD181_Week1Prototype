using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chowen
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float xInput;
        [SerializeField] private float zInput;

        [SerializeField] private float movementSpeed = 5f;

        [SerializeField] private Rigidbody rb;

        private void FixedUpdate()
        {
            xInput = Input.GetAxis("Horizontal");
            zInput = Input.GetAxis("Vertical");

            rb.AddForce(new Vector3(xInput, 0, zInput) * movementSpeed);
        }
    }
}