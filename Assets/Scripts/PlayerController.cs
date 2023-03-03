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

        [SerializeField] private float velocityMultiplier;

        private void FixedUpdate()
        {
            if (Timer.startGameCountdown)
            {
                xInput = Input.GetAxis("Horizontal");
                zInput = Input.GetAxis("Vertical");

                rb.AddForce(new Vector3(xInput, 0, zInput) * movementSpeed);
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, transform.position + rb.velocity * velocityMultiplier);
        }
    }
}